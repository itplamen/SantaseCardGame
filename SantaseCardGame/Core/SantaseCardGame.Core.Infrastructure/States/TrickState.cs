namespace SantaseCardGame.Core.Infrastructure.States
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class TrickState : ITrickState
    {
        private const int TRICK_CARDS = 2;

        private IDictionary<PlayerPosition, Card> cards = new Dictionary<PlayerPosition, Card>();

        public event Action OnPlay;

        public event Action OnClear;

        public event Action OnDisplay;

        public PlayerPosition PlayerTurn { get; set; }

        public CardSuit TrumpCardSuit { get; set; }

        public IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards => cards;

        public void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (cards.Count < TRICK_CARDS)
            {
                cards.Add(playerPosition, card);
                PlayerTurn = GetNextPlayerPosition(playerPosition);
            }

            OnDisplay?.Invoke();

            if (cards.Count == TRICK_CARDS)
            {
                OnPlay?.Invoke();
            }
        }

        public void Clear()
        {
            cards = new Dictionary<PlayerPosition, Card>();

            OnClear?.Invoke();
        }

        private PlayerPosition GetNextPlayerPosition(PlayerPosition current)
        {
            if (current == PlayerPosition.First)
            {
                return PlayerPosition.Second;
            }

            return PlayerPosition.First;
        }
    }
}