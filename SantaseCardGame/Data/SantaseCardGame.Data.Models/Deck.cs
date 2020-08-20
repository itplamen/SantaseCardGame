namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Deck
    {
        public Card TrumpCard { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>();

        public Card GetNextCard()
        {
            Card card = Cards[Cards.Count - 1];
            Cards.RemoveAt(Cards.Count - 1);

            return card;
        }
    }
}