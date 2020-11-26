namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayTrumpCard : IPlayLogic
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayTrumpCard(IDeckState deckState, ITrickState trickState)
        {
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public PlayerAction Play(Player player)
        {
            Card opponentCard = trickState.Cards.First(x => x.Key != player.Position).Value;

            if (ShouldPlayTrumpWhenFollowingSuit(player, opponentCard) ||
                ShouldPlayTrumpWhenNotFollowingSuit(player, opponentCard))
            {
                Card playCard = player.Cards
                    .OrderBy(x => x.Type)
                    .FirstOrDefault(x => x.Suit == trickState.TrumpCardSuit);

                if (playCard != null)
                {
                    return new PlayerAction(PlayerActionType.PlayCard, playCard);
                }
            }

            return new PlayerAction(PlayerActionType.None);
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