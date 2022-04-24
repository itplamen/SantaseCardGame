namespace SantaseCardGame.AI.Logic.Strategies.SecondPlayer
{
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayLowerCardStrategy : BasePlayerActionStrategy
    {
        public PlayLowerCardStrategy(ITrickState trickState)
            : base(trickState)
        {
        }

        protected override PlayerAction SelectStrategy(Player player)
        {
            Card card = player.Cards
                .Where(x => x.Suit == OpponentCard.Suit)
                .OrderBy(x => x.Type)
                .FirstOrDefault(x => x.Type < OpponentCard.Type);

            return new PlayerAction(PlayerActionType.PlayCard, card);
        }
    }
}
