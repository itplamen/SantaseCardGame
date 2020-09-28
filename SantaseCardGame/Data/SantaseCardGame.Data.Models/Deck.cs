namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Deck
    {
        public Card TrumpCard { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>();

        public Card GetNextCard()
        {
            Card card = Cards.First();
            Cards.RemoveAt(0);

            return card;
        }
    }
}