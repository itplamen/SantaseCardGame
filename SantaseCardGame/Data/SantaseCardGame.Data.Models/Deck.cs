namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Deck
    {
        private readonly List<Card> cards = new List<Card>();

        public Card TrumpCard => cards.Last();

        public IEnumerable<Card> Cards => cards;

        public void AddCard(Card card, int? index = null)
        {
            if (index.HasValue && index.Value >= 0)
            {
                cards.Insert(index.Value, card);
            }
            else
            {
                cards.Add(card);
            }
        }

        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        public Card GetNextCard()
        {
            Card card = cards.First();
            RemoveCard(card);

            return card;
        }
    }
}
