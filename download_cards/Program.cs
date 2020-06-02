using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;
using LibCAH.Card;


namespace download_cards
{
    class Program
    {
        static async Task  Main(string[] args)
        {
             var client = new HttpClient();
            String Url = "https://raw.githubusercontent.com/ajanata/PretendYoureXyzzy/master/cah_cards.sql";
            var result = await client.GetStringAsync(Url);
            Collection collection = new Collection();
            
            Regex WhiteCard = new Regex(@"^\d+\t+(?!\d\t)([^\t-]+)\t+([A-Z]+)\t*",RegexOptions.Multiline);
            foreach(Match M in WhiteCard.Matches(result)){
                collection.WhiteCards.Add(new White(M.Groups[1].Value, M.Groups[2].Value));
            }
            
            Regex BlackCard = new Regex(@"^\d+\t+(\d+)\t(\d+)\t([^\t-]+)\t+([A-Z]+)\t*",RegexOptions.Multiline);
            foreach(Match M in BlackCard.Matches(result)){
                collection.BlackCards.Add(new Black(M.Groups[3].Value, M.Groups[4].Value, Convert.ToInt32(M.Groups[1].Value), Convert.ToInt32(M.Groups[2].Value)));
            }

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented=true;
            await File.WriteAllTextAsync("cards.json", JsonSerializer.Serialize<Collection>(collection, options));            
            
        }
    }
}
