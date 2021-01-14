namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class TrickManager : ITrickManager
    {
        private readonly IGameState gameState;
        private readonly ITrickState trickState;
        private readonly ITrickWinner trickWinner;

        public TrickManager(IGameState gameState, ITrickState trickState, ITrickWinner trickWinner)
        {
            this.gameState = gameState;
            this.trickState = trickState;
            this.trickWinner = trickWinner;
        }

        public PlayerPosition Play(Game game)
        {
            if (trickState.Cards.Count() == gameState.TrickCards)
            {
                PlayerPosition winnerPosition = trickWinner.GetWinner(trickState.Cards, game.Deck.TrumpCard.Suit);
                Player winnerPlayer = game.Players.First(x => x.Position == winnerPosition);

                IEnumerable<Card> hand = trickState.Cards.Select(x => x.Value);
                winnerPlayer.Hands.Add(hand);

                trickState.PlayerTurn = winnerPosition;
                gameState.ShowMessage(winnerPosition, "Win");

                return winnerPosition;
            }

            return PlayerPosition.NoOne;
        }
    }
}