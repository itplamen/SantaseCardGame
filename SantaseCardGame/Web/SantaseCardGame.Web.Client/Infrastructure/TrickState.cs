namespace SantaseCardGame.Web.Client.Infrastructure
{
    using System;

    using SantaseCardGame.Data.Models;

    public class TrickState
    {
        public event Action OnPlayTrick;

        public event Action OnClearTrick;

        public event Action OnDisplayTrick;

        public Card FirstPlayedCard { get; set; }

        public Card SecondPlayedCard { get; set; }

        public PlayerPosition FirstToPlay { get; set; }

        public void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (FirstToPlay == playerPosition)
            {
                FirstPlayedCard = card;
            }
            else
            {
                SecondPlayedCard = card;
            }

            OnDisplayTrick?.Invoke();

            if (FirstPlayedCard != null && SecondPlayedCard != null)
            {
                OnPlayTrick?.Invoke();
            }
        }

        public void Clear()
        {
            FirstPlayedCard = null;
            SecondPlayedCard = null;

            OnClearTrick?.Invoke();
        }
    }
}