using System;

namespace LibCAH.Card {
    /// <summary>
    /// Black Cards
    /// </summary>
    public class Black {
        /// <summary>
        /// How many cards to draw *before* you play your card
        /// </summary>
        public int Draw { get; private set; }

        /// <summary>
        /// How many cards to pick when you play your card
        /// </summary>
        public int Pick { get; private set; }

        /// <summary>
        /// The Content of the card
        /// </summary>
        public String Text { get; private set; }

        /// <summary>
        /// The Pack the card belongs to
        /// </summary>
        public String Pack { get; private set; }

        /// <summary>
        /// Creates a Black Card Object
        /// </summary>
        /// <param name="Text">The Content of the card</param>
        /// <param name="Pack">The Pack the card belongs to</param>
        /// <param name="Draw">How many cards to draw *before* you play your card</param>
        /// <param name="Pick">How many cards to pick when you play your card</param>
        public Black (String Text, String Pack, int Draw, int Pick) {
            this.Text = Text;
            this.Pack = Pack;
            this.Draw = Draw;
            this.Pick = Pick;
        }
    }
}