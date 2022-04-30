﻿namespace SantaseCardGame.Core.Logic.GameStates
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Core.Logic.Contracts.Winning;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class MainGameHandler : IGameStateHandler
    {
        private readonly ICardsDealer dealer;
        private readonly IDeckState deckState;
        private readonly ITrickEndedValidator trickValidator;
        private readonly IEnumerable<IRoundWinner> roundWinners;

        public MainGameHandler(ICardsDealer dealer, IDeckState deckState, ITrickEndedValidator trickValidator, IEnumerable<IRoundWinner> roundWinners)
        {
            this.dealer = dealer;
            this.deckState = deckState;
            this.trickValidator = trickValidator;
            this.roundWinners = roundWinners;
        }

        public void Handle(Game game)
        {
            foreach (var roundWinner in roundWinners)
            {
                Round round = roundWinner.GetWinner(deckState.ClosedBy, game.Players);

                if (trickValidator.HasEnded() && 
                    game.Players.All(x => x.Cards.Any()) &&
                    round.WinnerPosition == PlayerPosition.None)
                {
                    dealer.DrawCards(game.Deck, game.Players);
                }
                else if (round.WinnerPosition != PlayerPosition.None)
                {
                    game.AddRound(round);
                }
            }
        }
    }
}