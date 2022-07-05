namespace SantaseCardGame.AI.Logic.Strategies.SecondPlayer
{
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayLowerCardStrategy : BasePlayerActionStrategy
    {
        private readonly ITrickState trickState;

        public PlayLowerCardStrategy(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        protected override PlayerAction SelectStrategy(Player player)
        {
            var opponentCard = trickState.Cards.First(x => x.Key != player.Position).Value;

            Card card = player.Cards
                .Where(x => x.Suit == opponentCard.Suit)
                .OrderBy(x => x.Type)
                .FirstOrDefault(x => x.Type < opponentCard.Type);

            return new PlayerAction(PlayerActionType.PlayCard, card);
        }
    }
}
