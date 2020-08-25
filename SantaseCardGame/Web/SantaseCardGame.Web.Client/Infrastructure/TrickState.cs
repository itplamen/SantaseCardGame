namespace SantaseCardGame.Web.Client.Infrastructure
{
    using System;

    using SantaseCardGame.Data.Models;

    public class TrickState
    {
        public event Action OnPlayTrick;

        public event Action OnClearTrick;

        public event Action OnDisplayTrick;

        public Card FirstPlayerCard { get; private set; }

        public Card SecondPlayerCard { get; private set; }

        public PlayerPosition FirstToPlay { get; set; }

        public CardSuit TrumpCardSuit { get; set; }

        public void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (playerPosition == PlayerPosition.First)
            {
                FirstPlayerCard = card;
            }
            else
            {
                SecondPlayerCard = card;
            }

            OnDisplayTrick?.Invoke();

            if (FirstPlayerCard != null && SecondPlayerCard != null)
            {
                OnPlayTrick?.Invoke();
            }
        }

        public void Clear()
        {
            FirstPlayerCard = null;
            SecondPlayerCard = null;

            OnClearTrick?.Invoke();
        }
    }
}