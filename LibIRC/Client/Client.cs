using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace LibIRC {

    /// <summary>
    /// A Class to connect to an IRC Server
    /// </summary>
    public partial class Client {

        // WebStream
        private TcpClient Connection;
        private Stream NetStream;
        private StreamReader Reader;

        private Config Configuration;
        private Dictionary<String, Channel> Channels;

        // Threading
        private ThreadSafeStruct<bool> ShouldClose;
        private ThreadSafeObject<Queue<String>> CommandQueue;
        private Thread NetThread;

        // Regex
        Regex ServerMessageRegex;
        Regex PrivateMessageRegex;

        private void BackThread () {
            while (!ShouldClose.Get ()) {
                // recieve Data
                if (Connection.Client.Poll (200, SelectMode.SelectRead)) {
                    String Line = Reader.ReadLine ();
                    Process (Line);
                }

                while (CommandQueue.ExecuteFunction (x => x.Count) > 0) {
                    Byte[] data = System.Text.Encoding.UTF8.GetBytes (CommandQueue.ExecuteFunction (x => x.Dequeue ()));
                    NetStream.Write (data, 0, data.Length);
                }
            }
            Byte[] d2 = System.Text.Encoding.UTF8.GetBytes (String.Format ("QUIT {0}\r\n", Configuration.QuitMessage));
            NetStream.Write (d2, 0, d2.Length);
        }

        /// <summary>
        /// A class used to connect to an IRC server
        /// </summary>
        /// <param name="config">The Configuration to use when connecting</param>
        public Client (Config config) {
            // Initialise regex BEFORE we connect
            // Nicks May Contain: 0-9A-Za-z_\-\[\]\{\}\\`\|
            // Server May Contain A-Za-z.\-
            ServerMessageRegex = new System.Text.RegularExpressions.Regex (@":([A-Za-z0-9\.\-]+) ([0-9]+) ([0-9A-Za-z_\-\[\]\{\}\\`\|]+) (.+)");
            PrivateMessageRegex = new System.Text.RegularExpressions.Regex (@":([0-9A-Za-z_\-\[\]\{\}\\`\|]+)!~([0-9A-Za-z_\-\[\]\{\}\\`\|]+)@([A-Za-z0-9\.\-:]+) PRIVMSG ([#0-9A-Za-z_\-\[\]\{\}\\`\|]+) :(.+)");

            Connection = new TcpClient (config.Host, config.Port);
            NetStream = Connection.GetStream ();
            Reader = new StreamReader (NetStream);
            this.Configuration = config;

            CommandQueue = new ThreadSafeObject<Queue<string>> (new Queue<string> ());
            ShouldClose = new ThreadSafeStruct<bool> (false);

            SendData (string.Format ("USER {0} * * {0}", config.Username));
            SendData (string.Format ("NICK {0}", config.Nick));

            NetThread = new Thread ((ThreadStart) delegate { this.BackThread (); });
            NetThread.Start ();
            Channels = new Dictionary<string, Channel> ();

        }

        /// <summary>
        /// Requests the Client Quits
        /// </summary>
        public void Quit () {
            ShouldClose.Set (true);
            NetThread.Join ();
        }

        /// <summary>
        /// Enqueues a string to be sent to the server
        /// </summary>
        /// <param name="Payload">The String to be sent</param>
        protected void SendData (String Payload) {
            CommandQueue.ExecuteFunction (x => { x.Enqueue (Payload + "\r\n"); return 0; });

        }

        /// <summary>
        /// Joins A channel
        /// /// </summary>
        /// <param name="ChannelName">The Name of the Channel To Join</param>
        /// <returns>A Channel Object for the Channel</returns>
        public Channel Join (String ChannelName) {
            if (!Channels.ContainsKey (ChannelName)) {
                Channel channel = new Channel (this, ChannelName);
                SendData (String.Format ("JOIN {0}", ChannelName));
                Channels[ChannelName] = channel;
            }
            return Channels[ChannelName];
        }
    }
}