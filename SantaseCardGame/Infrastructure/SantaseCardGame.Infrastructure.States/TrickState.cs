namespace SantaseCardGame.Infrastructure.States
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class TrickState : ITrickState
    {
        private const int TRICK_CARDS = 2;

        private readonly IDictionary<PlayerPosition, Card> cards = new Dictionary<PlayerPosition, Card>(TRICK_CARDS);

        public PlayerPosition PlayerTurn { get; private set; }

        public IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards => cards;

        public event Action OnDisplay;

        public event Action OnEnded;

        public void SetPlayerTurn(PlayerPosition playerPosition)
        {
            PlayerTurn = playerPosition;
        }

        public void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (cards.Count < TRICK_CARDS)
            {
                cards.Add(playerPosition, card);
                PlayerTurn = GetNextPlayerPosition(playerPosition);

                Display();
            }

            if (cards.Count == TRICK_CARDS)
            {
                OnEnded?.Invoke();
            }
        }

        public void Clear()
        {
            cards.Clear();
        }

        public void Display()
        {
            OnDisplay?.Invoke();
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
