namespace SantaseCardGame.Infrastructure.States
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class TrickState : ITrickState
    {
        private readonly IGameState gameState;
        private readonly IDictionary<PlayerPosition, Card> cards;

        public TrickState(IGameState gameState)
        {
            this.gameState = gameState;
            this.cards = new Dictionary<PlayerPosition, Card>(gameState.TrickCardsCount);
        }

        public PlayerPosition PlayerTurn { get; private set; }

        public IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards => cards;

        public event Action OnDisplay;

        public event Action OnManage;

        public void SetPlayerTurn(PlayerPosition playerPosition)
        {
            PlayerTurn = playerPosition;
        }

        public void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (cards.Count < gameState.TrickCardsCount)
            {
                cards.Add(playerPosition, card);
                PlayerTurn = GetNextPlayerPosition(playerPosition);

                Display();
            }

            OnManage?.Invoke();
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
            if (cards.Count < gameState.TrickCardsCount)
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
