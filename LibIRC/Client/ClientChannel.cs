using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace LibIRC {
    public partial class Client {
        /// <summary>
        /// Represents A Channel. This has to be a child class to avoid exposing sending arbritary data
        /// </summary>
        public class Channel {
            Client Parent;
            // Unfortunately, as C++ style friend classes in  have no equivalence in C#, internal is used for these terms
            internal StatusCode Response;
            internal ThreadSafeObject<Queue<Message>> MessageQueue;

            internal List<String> Inside_Users;
            internal bool EndOfNames;

            /// <summary>
            /// Gets a Collection of the Users
            /// </summary>
            public ReadOnlyCollection<String> Users { get { return Inside_Users.AsReadOnly (); } }

            /// <summary>
            /// The Name of the Channel
            /// </summary>
            public String ChannelName { get; private set; }

            internal Channel (Client Parent, String ChannelName) {
                this.ChannelName = ChannelName;
                this.Parent = Parent;
                Response = StatusCode.None;
                MessageQueue = new ThreadSafeObject<Queue<Message>> (new Queue<Message> ());
                Inside_Users = new List<string> ();
                EndOfNames = false;

            }

            /// <summary>
            /// Sends a Message to the Channel
            /// </summary>
            /// <param name="Message">The Message To send</param>
            public void SendMessage (String Message) {
                Parent.SendData (String.Format ("PRIVMSG {0} :{1}", ChannelName, Message));
            }

            /// <summary>
            /// Function to check if the channel has messages to be read
            /// </summary>
            /// <returns>If the channel has messages to be read</returns>
            public bool CanGetMessage () {
                return MessageQueue.ExecuteFunction (x => x.Count > 0);
            }

            /// <summary>
            /// Gets a Queued Message from the channel
            /// </summary>
            /// <returns>The Queued Message</returns>
            public Message GetMessage () {
                return MessageQueue.ExecuteFunction (x => x.Dequeue ());
            }
        }
    }
}