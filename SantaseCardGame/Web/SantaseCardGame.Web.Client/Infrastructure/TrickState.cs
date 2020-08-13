namespace SantaseCardGame.Web.Client.Infrastructure
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public class TrickState
    {
        private const int CARDS_COUNT = 2;

        public event Action OnPlayTrick;

        public ICollection<Card> Cards { get; private set; } = new List<Card>();

        public void AddCard(Card card)
        {
            if (Cards.Count <= CARDS_COUNT)
            {
                Cards.Add(card);

                if (Cards.Count == CARDS_COUNT)
                {
                    OnPlayTrick?.Invoke();
                }
            }
        }

        public void Clear()
        {
            Cards.Clear();
        }
    }
}