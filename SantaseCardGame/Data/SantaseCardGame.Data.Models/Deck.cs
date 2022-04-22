namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Deck
    {
        public Card TrumpCard { get; set; }

        public ICollection<Card> Cards { get; set; } = new List<Card>();

        public Card GetNextCard()
        {
            Card card = Cards.First();
            Cards.Remove(card);

            return card;
        }
    }
}
