using System;
using System.Collections.Generic;
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
            Match NumCommand = ServerMessageRegex.Match (Line);
            Match TextCommand = CommandRegex.Match (Line);
            if (NumCommand.Success) {
                //Console.WriteLine ("Server Message: " + Line);
                StatusCode status = (StatusCode) Convert.ToInt32 (NumCommand.Groups[2].Value);
                Match R;
                switch (status) {
                    case StatusCode.Welcome:
                        Had001.Set (true);
                        break;
                    case StatusCode.Motd:
                        Console.WriteLine (NumCommand.Groups[4].Value);
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
                        String channel = Regex.Match (NumCommand.Groups[4].Value, @"([#0-9A-Za-z_\-\[\]\{\}\\`\|]+) :.+").Groups[1].Value;
                        if (Channels.ContainsKey (channel)) {
                            Channels[channel].Response = status;
                        }
                        break;
                    case StatusCode.EndOfNames:

                        R = Regex.Match (NumCommand.Groups[4].Value, @"([#0-9A-Za-z_\-\[\]\{\}\\`\|\+]+) :(.+)");
                        if (Channels.ContainsKey (R.Groups[1].Value)) {
                            Channels[R.Groups[1].Value].EndOfNames = true;
                        }
                        break;
                    case StatusCode.NamReply:

                        R = Regex.Match (NumCommand.Groups[4].Value, @". ([#0-9A-Za-z_\-\[\]\{\}\\`\|\+]+) :(.+)");
                        if (Channels.ContainsKey (R.Groups[1].Value)) {
                            if (Channels[R.Groups[1].Value].EndOfNames) {
                                Channels[R.Groups[1].Value].EndOfNames = false;
                                Channels[R.Groups[1].Value].Inside_Users.Clear ();
                            }
                            foreach (String name in R.Groups[2].Value.Split (null)) {
                                Channels[R.Groups[1].Value].Inside_Users.Add (name);
                            }
                        }
                        break;

                    default:
                        Console.WriteLine ("{0}: {1}", status.ToString (), NumCommand.Groups[4].Value);
                        break;
                }

            } else if (TextCommand.Success) {
                try {
                    Command c = (Command) Enum.Parse (typeof (Command), TextCommand.Groups[4].Value);
                    switch (c) {
                        case Command.JOIN:
                            String ChannelName = TextCommand.Groups[5].Value;
                            if (ChannelName.StartsWith (":")) {
                                ChannelName = ChannelName.Substring (1);
                            }
                            if (TextCommand.Groups[1].Value == Configuration.Nick) {

                                Channels[ChannelName].Response = StatusCode.JOIN;

                            } else {
                                Console.WriteLine (TextCommand.Groups[1].Value);
                            }
                            SendData (String.Format ("NAMES {0}", ChannelName));
                            break;
                        case Command.PRIVMSG:
                            Match Priv= Regex.Match(TextCommand.Groups[5].Value, @"(\S+) :(.+)");
                            if (Priv.Groups[1].Value.StartsWith ("#")) { // Channel
                                        Message message = new Message (TextCommand.Groups[1].Value, Priv.Groups[2].Value);
                                        Channels[Priv.Groups[1].Value].MessageQueue.ExecuteFunction (q => { q.Enqueue (message); return 0; });
                                    } else { // DM
                                        DirectMessages.ExecuteFunction (x => {
                                            if (!x.ContainsKey (TextCommand.Groups[1].Value)) {
                                                x[TextCommand.Groups[1].Value] = new Queue<string> ();
                                            }
                                            Console.WriteLine (TextCommand.Groups[1].Value);
                                            x[TextCommand.Groups[1].Value].Enqueue (Priv.Groups[2].Value);
                                            return 0;
                                        });
                                    }            
                            break;
                        default:
                            Console.WriteLine ("Unhandled {0} Command", TextCommand.Groups[4].Value);
                            break;

                    }
                } catch (ArgumentException) {
                    Console.WriteLine ("Unrecognised {0} Command", TextCommand.Groups[4].Value);
                }
            } else {
                Console.WriteLine ("Unknown Message Type: " + Line);
            }
        }
    }
}