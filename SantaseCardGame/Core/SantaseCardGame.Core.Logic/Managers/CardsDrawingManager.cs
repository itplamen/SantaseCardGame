namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class CardsDrawingManager : ICardsDrawingManager
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;

        public CardsDrawingManager(IGameState gameState, IDeckState deckState)
        {
            this.gameState = gameState;
            this.deckState = deckState;
        }

        public void DrawCards(PlayerPosition winnerPosition, Game game)
        {
            if (deckState.ClosedBy == PlayerPosition.NoOne && game.Deck.Cards.Any())
            {
                Card firstCard = game.Deck.GetNextCard();
                Card secondCard = game.Deck.GetNextCard();

                Player winnerPlayer = game.Players.First(x => x.Position == winnerPosition);
                winnerPlayer.Cards.Add(firstCard);

                Player loserPlayer = game.Players.First(x => x.Position != winnerPosition);
                loserPlayer.Cards.Add(secondCard);

                deckState.CardsLeft = game.Deck.Cards.Count;
                deckState.ShouldFollowSuit = !game.Deck.Cards.Any();

                gameState.RenderBoard();
            }
        }
    }
}