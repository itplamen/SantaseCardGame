namespace SantaseCardGame.AI.Logic.Strategies.SecondPlayer
{
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayTrumpCardStrategy : BasePlayerActionStrategy
    {
        private readonly IDeckState deckState;

        public PlayTrumpCardStrategy(ITrickState trickState, IDeckState deckState)
            : base(trickState)
        {
            this.deckState = deckState;
        }

        protected override PlayerAction SelectStrategy(Player player)
        {
            if (ShouldPlayTrumpCardWhenFollowingSuit(player, OpponentCard) ||
                ShouldPlayTrumpCardWhenNotFollowingSuit(player, OpponentCard))
            {
                Card card = player.Cards
                    .Where(x => x.Suit == deckState.TrumpCard.Suit)
                    .OrderBy(x => x.Type)
                    .FirstOrDefault();

                return new PlayerAction(PlayerActionType.PlayCard, card);
            }

            return new PlayerAction(PlayerActionType.None);
        }

        private bool ShouldPlayTrumpCardWhenFollowingSuit(Player player, Card opponentCard)
        {
            return deckState.ShouldFollowSuit &&
                opponentCard.Suit != deckState.TrumpCard.Suit &&
                player.Cards.All(x => x.Suit != opponentCard.Suit);
        }

        private bool ShouldPlayTrumpCardWhenNotFollowingSuit(Player player, Card opponentCard)
        {
            return !deckState.ShouldFollowSuit &&
                opponentCard.Type >= CardType.Ten &&
                opponentCard.Suit != deckState.TrumpCard.Suit &&
                !player.Cards.Any(x => x.Suit == opponentCard.Suit && x.Type > opponentCard.Type);
        }
    }
}
