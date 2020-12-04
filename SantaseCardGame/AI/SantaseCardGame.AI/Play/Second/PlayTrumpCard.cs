namespace SantaseCardGame.AI.Play.Second
{
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class PlayTrumpCard : BasePlayLogic
    {
        private readonly IDeckState deckState;

        public PlayTrumpCard(ITrickState trickState, IDeckState deckState)
            : base(trickState)
        {
            this.deckState = deckState;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            if (ShouldPlayTrumpWhenFollowingSuit(player, OpponentCard) ||
                ShouldPlayTrumpWhenNotFollowingSuit(player, OpponentCard))
            {
                Card card = player.Cards
                    .Where(x => x.Suit == deckState.TrumpCard.Suit)
                    .OrderBy(x => x.Type)
                    .FirstOrDefault();

                return new PlayerAction(PlayerActionType.PlayCard, card);
            }

            return new PlayerAction(PlayerActionType.None);
        }

        private bool ShouldPlayTrumpWhenFollowingSuit(Player player, Card opponentCard)
        {
            return deckState.ShouldFollowSuit &&
                opponentCard.Suit != deckState.TrumpCard.Suit &&
                player.Cards.All(x => x.Suit != opponentCard.Suit);
        }

        private bool ShouldPlayTrumpWhenNotFollowingSuit(Player player, Card opponentCard)
        {
            return !deckState.ShouldFollowSuit &&
                opponentCard.Type >= CardType.Ten &&
                opponentCard.Suit != deckState.TrumpCard.Suit &&
                !player.Cards.Any(x => x.Suit == opponentCard.Suit && x.Type > opponentCard.Type);
        }
    }
}