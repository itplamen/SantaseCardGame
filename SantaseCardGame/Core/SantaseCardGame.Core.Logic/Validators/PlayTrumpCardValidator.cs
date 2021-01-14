namespace SantaseCardGame.Core.Logic.Validators
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class PlayTrumpCardValidator : IPlayCardValidator
    {
        private readonly IDeckState deckState;

        public PlayTrumpCardValidator(IDeckState deckState)
        {
            this.deckState = deckState;
        }

        public string Message => "You myst play trump card!";

        public bool CanPlay(Player player, Card card, Card opponentCard)
        {
            if (player.Cards.All(x => x.Suit != opponentCard.Suit) &&
                player.Cards.Any(x => x.Suit == deckState.TrumpCard.Suit))
            {
                return card.Suit == deckState.TrumpCard.Suit;
            }

            return true;
        }
    }
}