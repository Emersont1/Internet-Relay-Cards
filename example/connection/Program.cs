using System;
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
            Client.Channel channel = client.Join ("#test");
            while (true) {
                String Line = Console.ReadLine ();
                if (Line == "QUIT")
                    break;
                else
                    channel.SendMessage (Line);

            }
            client.Quit ();
            Console.WriteLine (config.ToJson ());
        }
    }
}