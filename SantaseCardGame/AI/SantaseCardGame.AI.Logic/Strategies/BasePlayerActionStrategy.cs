namespace SantaseCardGame.AI.Logic.Strategies
{
    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public abstract class BasePlayerActionStrategy : IPlayerActionStrategy
    {
        protected abstract PlayerAction SelectStrategy(Player player);

        public PlayerAction ChooseAction(Player player)
        {
            PlayerAction playerAction = SelectStrategy(player);

            if (playerAction.Type != PlayerActionType.None && playerAction.Card != null)
            {
                return playerAction;
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}
