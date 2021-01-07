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
        private readonly IDictionary<PlayerPosition, Card> cards;

        public TrickState(IGameState gameState)
        {
            this.gameState = gameState;
            this.cards = new Dictionary<PlayerPosition, Card>(gameState.TrickCards);
        }

        public event Action OnPlay;

        public event Action OnDisplay;

        public event Action OnManagePlayerTurn;

        public PlayerPosition PlayerTurn { get; set; }

        public IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards => new List<KeyValuePair<PlayerPosition, Card>>(cards);

        public async void AddCard(Card card, PlayerPosition playerPosition)
        {
            if (cards.Count < gameState.TrickCards)
            {
                cards.Add(playerPosition, card);
                PlayerTurn = GetNextPlayerPosition(playerPosition);
                OnDisplay?.Invoke();
            }

            if (cards.Count == gameState.TrickCards)
            {
                await Task.Delay(1500);
                OnPlay?.Invoke();

                cards.Clear();
                OnDisplay?.Invoke();
            }

            if (gameState.RoundWinner == PlayerPosition.NoOne)
            {
                OnManagePlayerTurn?.Invoke();
            }
        }

        private PlayerPosition GetNextPlayerPosition(PlayerPosition current)
        {
            if (cards.Count < gameState.TrickCards)
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