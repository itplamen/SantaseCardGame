namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class StatesManager : IStatesManager
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public StatesManager(IGameState gameState, IDeckState deckState, ITrickState trickState)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public void ResetStates()
        {
            deckState.CardsLeft = 0;
            deckState.TrumpCard = null;
            trickState.PlayerTurn = PlayerPosition.NoOne;

            ResetStatesToDefault();
        }

        public void SetRoundStates(PlayerPosition playerTurn, int cardsLeft, Card trump)
        {
            trickState.PlayerTurn = playerTurn;
            deckState.CardsLeft = cardsLeft;
            deckState.TrumpCard = trump;

            ResetStatesToDefault();
        }

        public void ResetPlayers(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                player.Cards.Clear();
                player.Hands.Clear();
                player.Announcements.Clear();
            }
        }

        private void ResetStatesToDefault()
        {
            gameState.RoundWinner = PlayerPosition.NoOne;
            deckState.ClosedBy = PlayerPosition.NoOne;
            deckState.ShouldFollowSuit = false;
            trickState.Cards.Clear();
        }
    }
}