using System;
using System.IO;
using System.Threading;
using LibIRC;

namespace cards_against_humanity {
    class Program {
        static void Main (string[] args) {
            if (!File.Exists ("config.json")) {
                Console.WriteLine ("File config.json does not exist! ... Exiting");
                return;
            }
            String Config_File = File.ReadAllText ("config.json");
            Config Configuration = Config.FromJson (Config_File);

            Client client = new Client (Configuration);
            Client.Channel c = client.Join ("##test");
            while (true) {
                SpinWait.SpinUntil (() => client.HasDirectMessages ());

                Console.WriteLine ("Someone's Slidin!: {0}", client.GetDirectMessage().Content);
            }
        }
    }
}