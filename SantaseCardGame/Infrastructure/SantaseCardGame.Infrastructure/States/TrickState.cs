namespace SantaseCardGame.Infrastructure.States
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class TrickState : ITrickState
    {
        private const int TRICK_CARDS = 2;

        private IDictionary<PlayerPosition, Card> cards = new Dictionary<PlayerPosition, Card>(TRICK_CARDS);

        public event Action OnPlay;

        public event Action OnClear;

        public event Action OnDisplay;

        public event Action OnManagePlayerTurn;

        public PlayerPosition PlayerTurn { get; set; }

        public IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards => cards;

        public void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (cards.Count < TRICK_CARDS)
            {
                cards.Add(playerPosition, card);
                PlayerTurn = GetNextPlayerPosition(playerPosition);

                OnManagePlayerTurn?.Invoke();
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
            if (cards.Count < TRICK_CARDS)
            {
                if (current == PlayerPosition.First)
                {
                    return PlayerPosition.Second;
                }

                return PlayerPosition.First;
            }

            return current;
        }
    }
}