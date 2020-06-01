using System;
using System.Threading;
using LibIRC;

namespace connection {
    class Program {
        static void Main (string[] args) {

            Config config = new Config ();
            Console.WriteLine (config.ToJson ());
            config.Host = "et1.uk";
            config.UseSSL = false;
            config.Username = "et1";
            config.Nick = "et1";
            config.Port = 6667;
            config.QuitMessage = "Test Shutdown";

            Client client = new Client (config);
            /*for(int i =0; i< 20; i++){
            Client.Channel channel = client.Join (String.Format("#{0}", i));
                   channel.SendMessage (String.Format("Line{0}", i));
            }*/

            Client.Channel channel = client.Join ("##c++");

            String Line = "";
            bool newline = false;
            while (true) {
                SpinWait.SpinUntil (() => channel.CanGetMessage () || Console.KeyAvailable);

                while (channel.CanGetMessage ()) {
                    Console.WriteLine (channel.GetMessage ().MessageText);
                }

                while (Console.KeyAvailable) {
                    ConsoleKeyInfo c = Console.ReadKey ();
                    if (c.Key == ConsoleKey.Enter) {
                        newline = true;
                        foreach (String s in channel.Users) {
                            Console.WriteLine (s);
                        }
                    } else {
                        Line += c.KeyChar;
                    }
                }
                if (newline) {
                    if (Line == "QUIT") {
                        break;
                    } else {
                        channel.SendMessage (Line);
                        Console.WriteLine (Line);
                    }
                    Line = "";
                    newline = false;

                }

            }

            client.Quit ();
            Console.WriteLine (config.ToJson ());
        }
    }
}