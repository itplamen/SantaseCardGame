namespace SantaseCardGame.Core.Logic.GameStates
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class StartOfRound : IGameStateHandler
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly ICardsDealer cardsDealer;

        public StartOfRound(IDeckState deckState, ITrickState trickState, ICardsDealer cardsDealer)
        {
            this.deckState = deckState;
            this.trickState = trickState;
            this.cardsDealer = cardsDealer;
        }

        public void Handle(Game game)
        {
            game.Players.ToList().ForEach(x => x.Clear());
            game.Deck = cardsDealer.Deal(game.Players.First(), game.Players.Last()); 

            PlayerPosition playerTurn = PlayerPosition.None;

            switch (game.Rounds.LastOrDefault()?.WinnerPosition)
            {
                case PlayerPosition.First:
                    playerTurn = PlayerPosition.Second;
                    break;
                case PlayerPosition.Second:
                    playerTurn = PlayerPosition.First;
                    break;
                default:
                    playerTurn = PlayerPosition.First;
                    break;
            }

            trickState.SetPlayerTurn(playerTurn);
            trickState.Clear();

            deckState.CardsLeft = game.Deck.Cards.Count();
            deckState.TrumpCard = game.Deck.TrumpCard;

            deckState.ClosedBy = PlayerPosition.None;
            deckState.ShouldFollowSuit = false;
        }
    }
}
