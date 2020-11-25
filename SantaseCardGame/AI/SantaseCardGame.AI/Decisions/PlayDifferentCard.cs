namespace SantaseCardGame.AI.Decisions
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayDifferentCard : IPlayCardDecision
    {
        private readonly ITrickState trickState;

        public PlayDifferentCard(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        public Card PlayCard(Player player, Card opponentCard)
        {
            if (player.Cards.All(x => x.Suit != opponentCard.Suit && x.Suit != trickState.TrumpCardSuit))
            {
                return player.Cards
                    .OrderBy(x => x.Type)
                    .FirstOrDefault();
            }

            return null;
        }
    }
}