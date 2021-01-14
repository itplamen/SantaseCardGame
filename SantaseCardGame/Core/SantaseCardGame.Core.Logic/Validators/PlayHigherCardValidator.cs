namespace SantaseCardGame.Core.Logic.Validators
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayHigherCardValidator : IPlayCardValidator
    {
        public string Message => "You must play higher card!";

        public bool CanPlay(Player player, Card card, Card opponentCard)
        {
            if (player.Cards.Any(x => x.Suit == opponentCard.Suit && x.Type > opponentCard.Type))
            {
                return card.Type > opponentCard.Type;
            }

            return true;
        }
    }
}