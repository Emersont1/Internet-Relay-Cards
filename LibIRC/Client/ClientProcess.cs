using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace LibIRC {
    public partial class Client {

        void Process (string Line) {
            if (Line.StartsWith ("PING ")) {
                String payload = "PONG " + Line.Substring (5);
                SendData (payload);
                return; // no further processing required
            }
            Match Server = ServerMessageRegex.Match (Line);
            Match Priv = PrivateMessageRegex.Match (Line);
            Match JoinConfirm = JoinConfirmRegex.Match (Line);
            if (Server.Success) {
                //Console.WriteLine ("Server Message: " + Line);
                StatusCode status = (StatusCode) Convert.ToInt32 (Server.Groups[2].Value);
                switch (status) {
                    case StatusCode.Welcome:
                        Had001.Set (true);
                        break;
                    case StatusCode.Motd:
                        Console.WriteLine (Server.Groups[4].Value);
                        //add to motd string
                        break;
                    case StatusCode.NicknameInUse:
                        Configuration.Nick += "_";
                        Console.WriteLine ("Nick Was In Use, Using {0}", Configuration.Nick);
                        SendData (string.Format ("NICK {0}", Configuration.Nick));
                        break;
                    case StatusCode.TooManyChannels:
                    case StatusCode.ChannelIsFull:
                    case StatusCode.UnknownMode:
                    case StatusCode.InviteOnlyChannel:
                    case StatusCode.BannedFromChannel:
                    case StatusCode.BadChannelKey:
                    case StatusCode.NoPrivileges:
                        String channel = Regex.Match (Server.Groups[4].Value, @"([#0-9A-Za-z_\-\[\]\{\}\\`\|]+) :.+").Groups[1].Value;
                        if (Channels.ContainsKey (channel)) {
                            Channels[channel].Response = StatusCode.TooManyChannels;
                        }
                        break;
                    default:
                        Console.WriteLine ("{0}: {1}", status.ToString (), Server.Groups[4].Value);
                        break;
                }

            } else if (Priv.Success) {
                if (Priv.Groups[4].Value.StartsWith ("#")) { // Channel
                    Message message = new Message (Priv.Groups[1].Value, Priv.Groups[5].Value);
                    Channels[Priv.Groups[4].Value].MessageQueue.ExecuteFunction (q => { q.Enqueue (message); return 0; });
                } else { // DM

                }
            } else if (JoinConfirm.Success) {
                Channels[JoinConfirm.Groups[4].Value].Response = StatusCode.JOIN;
            } else {
                Console.WriteLine ("Unknown Message Type: " + Line);
            }
        }
    }
}