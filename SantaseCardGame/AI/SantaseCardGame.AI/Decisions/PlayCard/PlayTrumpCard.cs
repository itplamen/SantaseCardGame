namespace SantaseCardGame.AI.Decisions.PlayCard
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayTrumpCard : IPlayCardDecision
    {
        private readonly ITrickState trickState;

        public PlayTrumpCard(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        public Card PlayCard(Player player, Card opponentCard)
        {
            if (opponentCard.Type >= CardType.Ten &&
                opponentCard.Suit != trickState.TrumpCardSuit &&
                !player.Cards.Any(x => x.Suit == opponentCard.Suit && x.Type > opponentCard.Type))
            {
                return player.Cards
                    .OrderBy(x => x.Type)
                    .FirstOrDefault(x => x.Suit == trickState.TrumpCardSuit);
            }

            return null;
        }
    }
}