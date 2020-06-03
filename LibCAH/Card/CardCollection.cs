using System;
using System.Collections.Generic;

namespace LibCAH.Card {
    /// <summary>
    /// Object For a Collection of White and Black Cards
    /// </summary>
    public class Collection {
        /// <summary>
        /// List of White cards
        /// </summary>
        public List<White> WhiteCards { get; set; }
        /// <summary>
        /// List of Black Cards
        /// </summary>
        public List<Black> BlackCards { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public Collection () {
            WhiteCards = new List<White> ();
            BlackCards = new List<Black> ();
        }
    }
}