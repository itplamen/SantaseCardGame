namespace SantaseCardGame.Core.Logic.Validators
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlaySameSuitCardValidator : IPlayCardValidator
    {
        public string Message => "You must follow suit!";

        public bool CanPlay(Player player, Card card, Card opponentCard)
        {
            if (player.Cards.Any(x => x.Suit == opponentCard.Suit))
            {
                return card.Suit == opponentCard.Suit;
            }

            return true;
        }
    }
}