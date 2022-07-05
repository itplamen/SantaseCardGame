namespace SantaseCardGame.Core.Logic.Validators.CardPlaying
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayTrumpCardValidator : ICardPlayableValidator
    {
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly IGameStorage gameStorage;

        public PlayTrumpCardValidator(IDeckState deckState, IGameState gameState, IGameStorage gameStorage)
        {
            this.deckState = deckState;
            this.gameState = gameState;
            this.gameStorage = gameStorage;
        }

        public string Message => "You must play trump card!";

        public bool CanPlay(Player player, Card card, Card opponentCard)
        {
            Game game = gameStorage.Get(gameState.CurrentGameId);

            if (deckState.ShouldFollowSuit &&
                player.Cards.Any(x => x.Suit == game.Deck.TrumpCard.Suit) &&
                player.Cards.All(x => x.Suit != opponentCard.Suit))
            {
                return card.Suit == game.Deck.TrumpCard.Suit;
            }

            return true;
        }
    }
}
