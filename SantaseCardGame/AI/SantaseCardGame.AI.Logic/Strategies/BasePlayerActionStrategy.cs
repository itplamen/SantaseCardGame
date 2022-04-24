namespace SantaseCardGame.AI.Logic.Strategies
{
    using System.Linq;

    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public abstract class BasePlayerActionStrategy : IPlayerActionStrategy
    {
        private readonly ITrickState trickState;

        protected BasePlayerActionStrategy(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        protected Card OpponentCard { get; private set; }

        protected abstract PlayerAction SelectStrategy(Player player);

        public PlayerAction ChooseAction(Player player)
        {
            OpponentCard = trickState.Cards.FirstOrDefault(x => x.Key != player.Position).Value;
            PlayerAction playerAction = SelectStrategy(player);

            if (playerAction.Type != PlayerActionType.None && playerAction.Card != null)
            {
                return playerAction;
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}
