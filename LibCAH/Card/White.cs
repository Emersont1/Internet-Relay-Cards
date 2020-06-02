using System;

namespace LibCAH.Card {
    /// <summary>
    /// White Cards
    /// </summary>
    public class White {
        /// <summary>
        /// The Content of the card
        /// </summary>
        public String Text { get; private set; }

        /// <summary>
        /// The Pack the card belongs to
        /// </summary>
        public String Pack { get; private set; }

        /// <summary>
        /// Creates a White Card object
        /// </summary>
        /// <param name="Text">The Content of the card</param>
        /// <param name="Pack">The Pack the Card Belongs to</param>
        public White (String Text, String Pack) {
            this.Text = Text;
            this.Pack = Pack;
        }
    }
}