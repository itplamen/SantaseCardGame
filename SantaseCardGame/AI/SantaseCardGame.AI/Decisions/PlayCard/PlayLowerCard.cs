namespace SantaseCardGame.AI.Decisions.PlayCard
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayLowerCard : IPlayCardDecision
    {
        public Card PlayCard(Player player, Card opponentCard)
        {
            return player.Cards
                .Where(x => x.Suit == opponentCard.Suit)
                .OrderBy(x => x.Type)
                .FirstOrDefault(x => x.Type < opponentCard.Type);
        }
    }
}