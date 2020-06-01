using System;

namespace LibIRC {
    /// <summary>
    /// 
    /// </summary>
    public class Message {
        /// <summary>
        /// The User Who Sent the message
        /// Possibly Create a class that contains Ops and privileges ?
        /// </summary>
        public String User { get; private set; }

        /// <summary>
        /// The Text of the message
        /// </summary>
        public String MessageText { get; private set; }

        /// <summary>
        /// Constructor for Message
        /// </summary>
        /// <param name="User">The User Who Sent the message</param>
        /// <param name="MessageText">The Text of the message</param>
        public Message (String User, String MessageText) {
            this.User = User;
            this.MessageText = MessageText;
        }
    }
}