namespace SantaseCardGame.Core.Logic.GameStates
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Winning;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class MainGameHandler : IGameStateHandler
    {
        private readonly ICardsDealer dealer;
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly IEnumerable<IRoundWinner> roundWinners;

        public MainGameHandler(ICardsDealer dealer, IDeckState deckState, IGameState gameState, IEnumerable<IRoundWinner> roundWinners)
        {
            this.dealer = dealer;
            this.deckState = deckState;
            this.gameState = gameState;
            this.roundWinners = roundWinners;
        }

        public void Handle(Game game)
        {
            foreach (var roundWinner in roundWinners)
            {
                var winner = roundWinner.GetWinner(deckState.ClosedBy, game.Players);
                
                if (winner.position != PlayerPosition.None)
                {
                    Round currentRound = game.Rounds.Last();
                    currentRound.WinnerPosition = winner.position;
                    currentRound.Points = winner.points;

                    gameState.RoundWinner = winner.position;
                    gameState.EndRound();

                    return;
                }
            }

            if (game.Deck != null &&
                game.Deck.Cards.Any() &&
                game.Players.Any(x => x.Hands.Any()) &&
                game.Players.All(x => x.Cards.Count() < gameState.RoundInitialCardsCount) &&
                deckState.ClosedBy == PlayerPosition.None)
            {
                dealer.DrawCards(game.Deck, game.Players);
            }
        }
    }
}
