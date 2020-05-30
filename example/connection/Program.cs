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

            Client c = new Client (config);
            Client.Channel chan = c.Join("#test");
            while(true){
                String Line = Console.ReadLine();
                if(Line=="QUIT")
                break;
                else
                chan.SendMessage(Line);

            }
            c.Quit();
            Console.WriteLine (config.ToJson ());
        }
    }
}