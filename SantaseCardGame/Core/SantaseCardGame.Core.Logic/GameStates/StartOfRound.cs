namespace SantaseCardGame.Core.Logic.GameStates
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class StartOfRound : IGameStateHandler
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly ICardsDealer cardsDealer;

        public StartOfRound(IGameState gameState, IDeckState deckState, ITrickState trickState, ICardsDealer cardsDealer)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.trickState = trickState;
            this.cardsDealer = cardsDealer;
        }

        public void Handle(Game game)
        {
            if (ShouldStartRound(game))
            {
                game.Players.ToList().ForEach(x => x.Clear());
                game.Deck = cardsDealer.Deal(game.Players.First(), game.Players.Last());

                PlayerPosition playerTurn = GetPlayerTurn(game);
                trickState.SetPlayerTurn(playerTurn);
                trickState.Clear();

                deckState.CardsLeft = game.Deck.Cards.Count();
                deckState.TrumpCard = game.Deck.TrumpCard;

                deckState.ClosedBy = PlayerPosition.None;
                deckState.ShouldFollowSuit = false;

                gameState.RoundWinner = PlayerPosition.None;
            }
        }

        private PlayerPosition GetPlayerTurn(Game game)
        {
            switch (game.Rounds.LastOrDefault()?.WinnerPosition)
            {
                case PlayerPosition.First:
                    return PlayerPosition.Second;
                case PlayerPosition.Second:
                    return PlayerPosition.First;
                default:
                    return PlayerPosition.First;
            }
        }

        private bool ShouldStartRound(Game game)
        {
            return IsBeginningOfGame(game) ||
                IsNewRound(game);
        }

        private bool IsBeginningOfGame(Game game)
        {
            return gameState.RoundWinner == PlayerPosition.None &&
                game.Players.All(x => !x.Cards.Any()) &&
                game.Deck == null;
        }

        private bool IsNewRound(Game game)
        {
            return gameState.RoundWinner != PlayerPosition.None &&
                game.Rounds.Any();
        }
    }
}
