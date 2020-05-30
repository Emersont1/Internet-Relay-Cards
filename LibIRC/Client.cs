using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace LibIRC {

    /// <summary>
    /// A Class to connect to an IRC Server
    /// </summary>
    public class Client {
        private TcpClient connection;
        private Stream stream;
        private StreamReader reader;
        private ThreadSafeStruct<bool> ShouldClose;
        private ThreadSafeObject<Queue<String>> CommandQueue;
        private Thread thread;
        private Config config;

        private void BackThread () {
            while (!ShouldClose.Get ()) {
                // recieve Data
                if (connection.Client.Poll (2000, SelectMode.SelectRead)) {
                    String Line = reader.ReadLine ();

                    Console.WriteLine (Line);
                    if (Line.StartsWith ("PING ")) {
                        String payload = "PONG " + Line.Substring (5);
                        Console.WriteLine (payload);
                        SendData (payload);
                    }
                }

                while (CommandQueue.ExecuteFunction (x => x.Count) > 0) {
                    Byte[] data = System.Text.Encoding.UTF8.GetBytes (CommandQueue.ExecuteFunction (x => x.Dequeue ()));
                    stream.Write (data, 0, data.Length);
                }
            }
            Byte[] d2 = System.Text.Encoding.UTF8.GetBytes (String.Format ("QUIT {0}\r\n", config.QuitMessage));
            stream.Write (d2, 0, d2.Length);
        }

        /// <summary>
        /// A class used to connect to an IRC server
        /// </summary>
        /// <param name="config">The Configuration to use when connecting</param>
        public Client (Config config) {
            connection = new TcpClient (config.Host, config.Port);
            stream = connection.GetStream ();
            reader = new StreamReader (stream);
            this.config = config;

            CommandQueue = new ThreadSafeObject<Queue<string>> (new Queue<string> ());
            ShouldClose = new ThreadSafeStruct<bool> (false);

            SendData (string.Format ("USER {0} * * {0}", config.Username));
            SendData (string.Format ("NICK {0}", config.Nick));

            thread = new Thread ((ThreadStart) delegate { this.BackThread (); });
            thread.Start ();
        }

        /// <summary>
        /// Requests the Client Quits
        /// </summary>
        public void Quit () {
            ShouldClose.Set (true);
            thread.Join ();
        }

        /// <summary>
        /// Enqueues a string to be sent to the server
        /// </summary>
        /// <param name="Payload">The String to be sent</param>
        protected void SendData (String Payload) {
            CommandQueue.ExecuteFunction (x => { x.Enqueue (Payload + "\r\n"); return 0; });

        }
    }
}