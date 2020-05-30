using System;
using LibIRC;

namespace connection {
    class Program {
        static void Main (string[] args) {

            Config config = new Config();
            Console.WriteLine (config.ToJson ());
            config.Host = "et1.uk";
            config.UseSSL = false;
            config.Username = "et1";
            config.Nick = "et1";
            config.Port = 6667;

            Console.WriteLine (config.ToJson ());
        }
    }
}