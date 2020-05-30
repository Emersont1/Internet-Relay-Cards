using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace LibIRC {
    public partial class Client {
        /// <summary>
        /// Represents A Channel or DM.static This has to be a child class to avoid exposing sending arbritary data
        /// </summary>
        public class Channel {
            Client parent;

            /// <summary>
            /// The Name of the Channel
            /// </summary>
            public String ChannelName { get; private set; }

            internal Channel (Client parent, String ChannelName) {
                this.ChannelName = ChannelName;
                this.parent = parent;
            }

            /// <summary>
            /// Sends a Message to the Channel
            /// </summary>
            /// <param name="message">The Message To send</param>
            public void SendMessage (String message) {
                parent.SendData (String.Format ("PRIVMSG {0} :{1}", ChannelName, message));
            }
        }
    }
}