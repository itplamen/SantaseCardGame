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
        private readonly ITrickState trickState;
        private readonly IEnumerable<IRoundWinner> roundWinners;

        public MainGameHandler(ICardsDealer dealer, IDeckState deckState, ITrickState trickState, IEnumerable<IRoundWinner> roundWinners)
        {
            this.dealer = dealer;
            this.deckState = deckState;
            this.trickState = trickState;
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
                game.Players.All(x => x.Cards.Count() < 6) &&
                round.WinnerPosition == PlayerPosition.None)
            {
                dealer.DrawCards(game.Deck, game.Players);

                trickState.Display();
            }
            else if (round.WinnerPosition != PlayerPosition.None)
            {
                game.AddRound(round);
            }
        }
    }
}
