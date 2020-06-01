using System;

namespace LibIRC {
    /// <summary>
    /// Textual Commands sent from the server
    /// </summary>
    public enum Command {
        ///<summary>
        /// A User Has Joined the channel
        ///</summary>
        JOIN = 1025,
        ///<summary>
        /// A User has left the channel
        ///</summary>
        PART = 1026,
        ///<summary>
        /// A User has sent a message
        ///</summary>
        PRIVMSG = 1027
    }
}