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
            Round round = new Round();

            foreach (var roundWinner in roundWinners)
            {
                round = roundWinner.GetWinner(deckState.ClosedBy, game.Players);
                if (round.WinnerPosition != PlayerPosition.None)
                {
                    break;
                }
            }

            if (game.Deck != null &&
                game.Deck.Cards.Any() &&
                game.Players.Any(x => x.Hands.Any()) &&
                game.Players.All(x => x.Cards.Count() < gameState.RoundInitialCardsCount) &&
                round.WinnerPosition == PlayerPosition.None &&
                deckState.ClosedBy == PlayerPosition.None)
            {
                dealer.DrawCards(game.Deck, game.Players);
            }
            else if (round.WinnerPosition != PlayerPosition.None)
            {
                game.AddRound(round);

                gameState.RoundWinner = round.WinnerPosition;
                gameState.EndRound();
            }
        }
    }
}
