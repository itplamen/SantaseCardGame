namespace SantaseCardGame.Core.Logic.Validators.CardPlaying
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlaySameSuitCardValidator : ICardPlayableValidator
    {
        private readonly IDeckState deckState;

        public PlaySameSuitCardValidator(IDeckState deckState)
        {
            this.deckState = deckState;
        }

        public string Message => "You must follow suit!";

        public bool CanPlay(Player player, Card card, Card opponentCard)
        {
            if (deckState.ShouldFollowSuit && 
                player.Cards.Any(x => x.Suit == opponentCard.Suit))
            {
                return card.Suit == opponentCard.Suit;
            }

            return true;
        }
    }
}
