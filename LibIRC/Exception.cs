using System;

namespace LibIRC {
    /// <summary>
    /// Exception for IRC errors
    /// </summary>
    public class IrcException : Exception {
        /// <summary>
        /// The Error code
        /// </summary>
        public StatusCode ErrorCode;

        /// <summary>
        /// Constructor for the exception
        /// </summary>
        /// <param name="ErrorCode">The Error Code</param>
        public IrcException (StatusCode ErrorCode) : base (String.Format ("A {0} error occoured", ErrorCode)) { }
    }
}