namespace SantaseCardGame.Core.Logic.GameStates
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Core.Logic.Contracts.Winning;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class EndOfTrickHandler : IGameStateHandler
    {
        private readonly ITrickState trickState;
        private readonly ITrickWinner trickWinner;
        private readonly ITrickEndedValidator trickValidator;

        public EndOfTrickHandler(ITrickState trickState, ITrickWinner trickWinner, ITrickEndedValidator trickValidator)
        {
            this.trickState = trickState;
            this.trickWinner = trickWinner;
            this.trickValidator = trickValidator;
        }

        public void Handle(Game game)
        {
            if (trickValidator.HasEnded())
            {
                PlayerPosition winnerPosition = trickWinner.GetWinner(trickState.Cards, game.Deck.TrumpCard.Suit);
                Player winnerPlayer = game.Players.First(x => x.Position == winnerPosition);

                IEnumerable<Card> hand = trickState.Cards.Select(x => x.Value).ToList();
                winnerPlayer.AddHand(hand);

                trickState.SetPlayerTurn(winnerPosition);
                trickState.Clear();
            }
        }
    }
}
