namespace SantaseCardGame.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Deck
    {
        public Card TrumpCard => Cards.Last();

        public ICollection<Card> Cards { get; set; } = new List<Card>();

        public PlayerPosition ClosedBy { get; set; }

        public Card GetNextCard()
        {
            if (!Cards.Any())
            {
                throw new InvalidOperationException("Cannot remove card from an empty deck!");
            }

            Card card = Cards.First();
            Cards.Remove(card);

            return card;
        }
    }
}
