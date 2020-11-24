namespace SantaseCardGame.AI.Decisions.PlayCard
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayTrumpCard : IPlayCardDecision
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayTrumpCard(IDeckState deckState, ITrickState trickState)
        {
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public Card PlayCard(Player player, Card opponentCard)
        {
            if (ShouldPlayTrumpWhenFollowingSuit(player, opponentCard) ||
                ShouldPlayTrumpWhenNotFollowingSuit(player, opponentCard))
            {
                return player.Cards
                    .OrderBy(x => x.Type)
                    .FirstOrDefault(x => x.Suit == trickState.TrumpCardSuit);
            }

            return null;
        }

        private bool ShouldPlayTrumpWhenFollowingSuit(Player player, Card opponentCard)
        {
            return deckState.ShouldFollowSuit &&
                opponentCard.Suit != trickState.TrumpCardSuit &&
                player.Cards.All(x => x.Suit != opponentCard.Suit);
        }

        private bool ShouldPlayTrumpWhenNotFollowingSuit(Player player, Card opponentCard)
        {
            return !deckState.ShouldFollowSuit &&
                opponentCard.Type >= CardType.Ten &&
                opponentCard.Suit != trickState.TrumpCardSuit &&
                !player.Cards.Any(x => x.Suit == opponentCard.Suit && x.Type > opponentCard.Type);
        }
    }
}