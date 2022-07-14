namespace SantaseCardGame.Core.Logic.GameStates
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Winning;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class EndOfTrickHandler : IGameStateHandler
    {
        private readonly IGameState gameState;
        private readonly ITrickState trickState;
        private readonly ITrickWinner trickWinner;

        public EndOfTrickHandler(IGameState gameState, ITrickState trickState, ITrickWinner trickWinner)
        {
            this.gameState = gameState;
            this.trickState = trickState;
            this.trickWinner = trickWinner;
        }

        public void Handle(Game game)
        {
            if (trickState.Cards.Count() == gameState.TrickCardsCount)
            {
                PlayerPosition winnerPosition = trickWinner.GetWinner(trickState.Cards, game.Deck.TrumpCard);
                Player winnerPlayer = game.Players.First(x => x.Position == winnerPosition);

                IEnumerable<Card> hand = trickState.Cards.Select(x => x.Value).ToList();
                winnerPlayer.Hands.Add(hand);

                trickState.SetPlayerTurn(winnerPosition);
                trickState.Clear();

                gameState.ShowMessage(winnerPosition, "Win");
            }
        }
    }
}
