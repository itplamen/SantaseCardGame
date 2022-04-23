namespace SantaseCardGame.Core.Logic.Validators.CardPlaying
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayTrumpCardValidator : ICardPlayableValidator
    {
        private readonly IDeckState deckState;

        public PlayTrumpCardValidator(IDeckState deckState)
        {
            this.deckState = deckState;
        }

        public bool CanPlay(Player player, Card card, Card opponentCard)
        {
            if (deckState.ShouldFollowSuit &&
                player.Cards.Any(x => x.Suit == deckState.TrumpCardSuit) &&
                player.Cards.All(x => x.Suit != opponentCard.Suit))
            {
                return card.Suit == deckState.TrumpCardSuit;
            }

            return true;
        }
    }
}
