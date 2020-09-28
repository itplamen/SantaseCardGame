namespace SantaseCardGame.Core.Infrastructure.States
{
    using System;
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class TrickState : ITrickState
    {
        private const int TRICK_CARDS = 2;

        public event Action OnPlay;

        public event Action OnClear;

        public event Action OnDisplay;

        public Card[] Cards { get; private set; } = new Card[TRICK_CARDS];

        public PlayerPosition PlayerTurn { get; set; }

        public CardSuit TrumpCardSuit { get; set; }

        public void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (playerPosition == PlayerPosition.First)
            {
                Cards[0] = card;
                PlayerTurn = PlayerPosition.Second;
            }
            else
            {
                Cards[1] = card;
                PlayerTurn = PlayerPosition.First;
            }

            OnDisplay?.Invoke();

            if (Cards.All(x => x != null))
            {
                OnPlay?.Invoke();
            }
        }

        public void Clear()
        {
            Cards = new Card[TRICK_CARDS];

            OnClear?.Invoke();
        }
    }
}