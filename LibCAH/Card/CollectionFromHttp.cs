using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace LibCAH.Card {
    /// <summary>
    /// Class for Loading things
    /// </summary>
    public static class Load {
        /// <summary>
        /// Loads Cards from Http Xyxxy style
        /// </summary>
        /// <param name="Url">The URL</param>
        public static Collection XyxxyFromHttp (String Url) {
            var client = new HttpClient ();
            var task = client.GetStringAsync (Url);
            task.Wait ();
            var result = task.Result;
            Collection collection = new Collection ();

            Regex WhiteCard = new Regex (@"^\d+\t+(?!\d\t)([^\t-]+)\t+([A-Z]+)\t*", RegexOptions.Multiline);
            foreach (Match M in WhiteCard.Matches (result)) {
                collection.WhiteCards.Add (new White (M.Groups[1].Value, M.Groups[2].Value));
            }

            Regex BlackCard = new Regex (@"^\d+\t+(\d+)\t(\d+)\t([^\t-]+)\t+([A-Z]+)\t*", RegexOptions.Multiline);
            foreach (Match M in BlackCard.Matches (result)) {
                collection.BlackCards.Add (new Black (M.Groups[3].Value, M.Groups[4].Value, Convert.ToInt32 (M.Groups[1].Value), Convert.ToInt32 (M.Groups[2].Value)));
            }
            return collection;
        }
    }
}