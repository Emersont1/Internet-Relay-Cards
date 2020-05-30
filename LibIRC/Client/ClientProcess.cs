using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace LibIRC {
    public partial class Client {

        void Process (string Line) {
            if (Line.StartsWith ("PING ")) {
                String payload = "PONG " + Line.Substring (5);
                Console.WriteLine (payload);
                SendData (payload);
                return; // no further processing required
            }
            Match server = ServerMessageRegex.Match (Line);
            Match priv = PrivateMessageRegex.Match (Line);
            if (server.Success) {
                Console.WriteLine ("Server Message");

            } else if (priv.Success) {
                Console.WriteLine ("Channel Message");
            } else {
                Console.WriteLine ("Unknown Message Type");
            }
        }
    }
}