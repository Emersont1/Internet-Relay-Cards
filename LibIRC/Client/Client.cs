using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace LibIRC {

    /// <summary>
    /// Class to Represent a Direct Message
    /// </summary>
    public class DirectMessage {

        /// <summary>
        /// The Sender of the Message
        /// </summary>
        public string From;

        /// <summary>
        /// The Content of the Message
        /// </summary>
        public string Content;
    }

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
        private ThreadSafeObject<Queue<DirectMessage>> DirectMessages;

        // Threading
        private ThreadSafeStruct<bool> ShouldClose;
        private ThreadSafeStruct<bool> Had001;
        private ThreadSafeObject<Queue<String>> CommandQueue;
        private Thread NetThread;

        // Regex
        Regex ServerMessageRegex;
        Regex CommandRegex;

        private void BackThread () {
            while (!ShouldClose.Get ()) {

                SpinWait.SpinUntil (() => CommandQueue.ExecuteFunction (x => x.Count) > 0 || Connection.Available > 0);
                if (Connection.Available > 0) {
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
        /// <param name="Configuration">The Configuration to use when connecting</param>
        public Client (Config Configuration) {
            // Initialise regex BEFORE we connect
            ServerMessageRegex = new Regex (@":([A-Za-z0-9\.\-]+) ([0-9]+) ([0-9A-Za-z_\-\[\]\{\}\\`\|\*]+) (.+)");
            CommandRegex = new Regex (@":([0-9A-Za-z_\-\[\]\{\}\\`\|]+)!~([0-9A-Za-z_\-\[\]\{\}\\`\|]+)@([A-Za-z0-9\.\-:\/]+) ([A-Z]+) (.+)");

            Connection = new TcpClient (Configuration.Host, Configuration.Port);
            Connection.NoDelay = true;
            NetStream = Connection.GetStream ();
            Reader = new StreamReader (NetStream);
            this.Configuration = Configuration;

            CommandQueue = new ThreadSafeObject<Queue<string>> (new Queue<string> ());
            ShouldClose = new ThreadSafeStruct<bool> (false);

            SendData (string.Format ("USER {0} * * {0}", Configuration.Username));
            SendData (string.Format ("NICK {0}", Configuration.Nick));

            NetThread = new Thread ((ThreadStart) delegate { this.BackThread (); });
            Channels = new Dictionary<string, Channel> ();
            DirectMessages = new ThreadSafeObject<Queue<DirectMessage>> (new Queue<DirectMessage> ());

            Had001 = new ThreadSafeStruct<bool> (false);

            // Start Thread
            NetThread.Start ();
            // Wait for 001;
            SpinWait.SpinUntil (() => Had001.Get ());
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
                Channels[ChannelName] = channel;
                SendData (String.Format ("JOIN {0}", ChannelName));
                SpinWait.SpinUntil (() => channel.Response != StatusCode.None);
                if (channel.Response == StatusCode.JOIN) { return channel; }
                Channels.Remove (ChannelName);
                throw new IrcException (channel.Response);
            } else {
                return Channels[ChannelName];
            }
        }

        /// <summary>
        /// Sends a DM
        /// </summary>
        /// <param name="User">The User to send the message to</param>
        /// <param name="Message">The Message To Send</param>
        public void SendDM (String User, String Message) {
            SendData (String.Format ("PRIVMSG {0} :{1}", User, Message));
        }

        /// <summary>
        /// Wether There are DMs to process
        /// </summary>
        public bool HasDirectMessages () {
            return DirectMessages.ExecuteFunction (x => x.Count > 0);
        }

        /// <summary>
        /// Gets the First Queued DM
        /// </summary>
        public DirectMessage GetDirectMessage () {
            return DirectMessages.ExecuteFunction (x => x.Dequeue ());
        }
    }
}