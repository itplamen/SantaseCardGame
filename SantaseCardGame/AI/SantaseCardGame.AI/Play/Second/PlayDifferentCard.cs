namespace SantaseCardGame.AI.Play.Second
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayDifferentCard : BasePlayLogic
    {
        private readonly ITrickState trickState;

        public PlayDifferentCard(ITrickState trickState)
            : base(trickState)
        {
            this.trickState = trickState;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            if (player.Cards.All(x => x.Suit != OpponentCard.Suit && x.Suit != trickState.TrumpCardSuit))
            {
                Card card = player.Cards
                    .OrderBy(x => x.Type)
                    .FirstOrDefault();

                return new PlayerAction(PlayerActionType.PlayCard, card);
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}