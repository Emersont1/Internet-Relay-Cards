using System;
using System.Collections.Generic;
using System.Text.Json;
using LibCAH.Card;

namespace LibCAH.Game {
    /// <summary>
    /// Representation of a player
    /// </summary>
    public class Player {
        /// <summary>
        /// The cards the player has
        /// There's no Get; Set; so other players can't see their hand
        /// </summary>
        public List<White> Hand;

        /// <summary>
        /// The score they have
        /// </summary>
        public int Score;

        /// <summary>
        /// The User's nickname
        /// </summary>
        public String Nick;

        String GetHand () {
            return JsonSerializer.Serialize<List<White>> (Hand);
        }
    }
}