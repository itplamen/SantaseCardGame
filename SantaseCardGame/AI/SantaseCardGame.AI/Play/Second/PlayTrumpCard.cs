namespace SantaseCardGame.AI.Play.Second
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayTrumpCard : BasePlayLogic
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayTrumpCard(IDeckState deckState, ITrickState trickState)
            : base(trickState)
        {
            this.deckState = deckState;
            this.trickState = trickState;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            if (ShouldPlayTrumpWhenFollowingSuit(player, OpponentCard) ||
                ShouldPlayTrumpWhenNotFollowingSuit(player, OpponentCard))
            {
                Card card = player.Cards
                    .OrderBy(x => x.Type)
                    .FirstOrDefault(x => x.Suit == trickState.TrumpCardSuit);

                return new PlayerAction(PlayerActionType.PlayCard, card);
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