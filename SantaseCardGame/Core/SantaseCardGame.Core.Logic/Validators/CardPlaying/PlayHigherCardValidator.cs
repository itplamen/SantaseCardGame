namespace SantaseCardGame.Core.Logic.Validators.CardPlaying
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayHigherCardValidator : ICardPlayableValidator
    {
        private readonly IDeckState deckState;

        public PlayHigherCardValidator(IDeckState deckState)
        {
            this.deckState = deckState;
        }

        public string Message => "You must play higher card!";

        public bool CanPlay(Player player, Card card, Card opponentCard)
        {
            if (deckState.ShouldFollowSuit && 
                player.Cards.Any(x => x.Suit == opponentCard.Suit && x.Type > opponentCard.Type))
            {
                return card.Type > opponentCard.Type;
            }

            return true;
        }
    }
}
