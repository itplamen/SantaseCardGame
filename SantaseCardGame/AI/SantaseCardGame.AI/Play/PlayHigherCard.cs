namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayHigherCard : IPlayCard
    {
        public Card PlayCard(Player player, Card opponentCard)
        {
            return player.Cards
                .Where(x => x.Suit == opponentCard.Suit)
                .OrderByDescending(x => x.Type)
                .FirstOrDefault(x => x.Type > opponentCard.Type);
        }
    }
}