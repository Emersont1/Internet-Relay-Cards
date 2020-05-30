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
            Match Server = ServerMessageRegex.Match (Line);
            Match Priv = PrivateMessageRegex.Match (Line);
            if (Server.Success) {
                Console.WriteLine ("Server Message: " + Line);
                StatusCode status = (StatusCode) Convert.ToInt32 (Server.Groups[2].Value);
                switch (status) {
                    case StatusCode.Motd:
                        //add to motd string
                        break;
                    default:
                        Console.WriteLine (status.ToString ());
                        break;
                }

            } else if (Priv.Success) {
                Console.WriteLine ("Channel Message: " + Line);
            } else {
                Console.WriteLine ("Unknown Message Type: " + Line);
            }
        }
    }
}