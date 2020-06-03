using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using LibCAH;

namespace LibCAH.Game {
    /// <summary>
    /// An Instance of the Game
    /// </summary>
    public class Instance {
        /// <summary>
        /// List of the Players
        /// </summary>
        public List<Player> Players;

        /// <summary>
        /// The Deck of Cards to be Drawn From
        /// </summary>
        public Queue<Card.White> WDeck;

        /// <summary>
        /// Where Discarded Cards go once they've been used
        /// </summary>
        public List<Card.White> WDiscard;

        /// <summary>
        /// The Deck of Cards to be Drawn From
        /// </summary>
        public Queue<Card.Black> BDeck;

        /// <summary>
        /// Where Discarded Cards go once they've been used
        /// </summary>
        public List<Card.Black> BDiscard;

        /// <summary>
        /// Draws a white Card
        /// </summary>
        /// <returns></returns>
        public Card.White DrawCard () {
            // repopulate deck
            if (WDeck.Count == 0) {
                List<Card.White> Cards = WDiscard;
                WDiscard.Clear ();
                Random r = new Random ();
                WDeck = new Queue<Card.White> (Cards.OrderBy (x => r.Next ()));
            }
            return WDeck.Dequeue ();
        }

        /// <summary>
        /// Draws a Black Card
        /// </summary>
        /// <returns></returns>
        public Card.Black GetBlackCard () {
            // repopulate deck
            if (BDeck.Count == 0) {
                List<Card.Black> Cards = BDiscard;
                BDiscard.Clear ();
                Random r = new Random ();
                BDeck = new Queue<Card.Black> (Cards.OrderBy (x => r.Next ()));
            }
            return
            BDeck.Dequeue ();
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="WhiteDeck"></param>
        /// <param name="BlackDeck"></param>
        public Instance (List<Card.White> WhiteDeck, List<Card.Black> BlackDeck) {
            Players = new List<Player> ();
            WDeck = new Queue<Card.White> ();
            WDiscard = WhiteDeck;
        }

        String GetScore () {
            return JsonSerializer.Serialize<List<Player>> (Players);
        }
    }
}