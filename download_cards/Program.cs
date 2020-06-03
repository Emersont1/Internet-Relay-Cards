using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;
using LibCAH.Card;


namespace download_cards
{
    class Program
    {
        static void Main(string[] args)
        {   
            String Url = "https://raw.githubusercontent.com/ajanata/PretendYoureXyzzy/master/cah_cards.sql";
            
            Collection collection = Load.XyxxyFromHttp(Url);

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented=true;
            var task= File.WriteAllTextAsync("cards.json", JsonSerializer.Serialize<Collection>(collection, options));        
            task.Wait();    
            
        }
    }
}
