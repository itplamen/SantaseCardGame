namespace SantaseCardGame.Infrastructure.States
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class TrickState : ITrickState
    {
        private readonly IGameState gameState;

        public TrickState(IGameState gameState)
        {
            this.gameState = gameState;
            this.Cards = new Dictionary<PlayerPosition, Card>(gameState.TrickCards);
        }

        public event Action OnPlay;

        public event Action OnDisplay;

        public event Action OnGamePlayerTurn;

        public PlayerPosition PlayerTurn { get; set; }

        public IDictionary<PlayerPosition, Card> Cards { get; private set; }

        public async void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (Cards.Count < gameState.TrickCards)
            {
                Cards.Add(playerPosition, card);
                PlayerTurn = GetNextPlayerPosition(playerPosition);
                OnDisplay?.Invoke();
            }

            if (Cards.Count == gameState.TrickCards)
            {
                await Task.Delay(gameState.SimulateDelay);
            }

            OnPlay?.Invoke();

            if (Cards.Count == gameState.TrickCards || gameState.RoundWinner != PlayerPosition.NoOne)
            {
                if (gameState.RoundWinner != PlayerPosition.NoOne && Cards.Count < gameState.TrickCards)
                {
                    await Task.Delay(gameState.SimulateDelay);
                }

                Cards.Clear();
                OnDisplay?.Invoke();
            }

            if (PlayerTurn == PlayerPosition.First && gameState.RoundWinner == PlayerPosition.NoOne)
            {
                OnGamePlayerTurn?.Invoke();
            }
        }

        private PlayerPosition GetNextPlayerPosition(PlayerPosition current)
        {
            if (Cards.Count < gameState.TrickCards)
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