namespace SantaseCardGame.Core.Logic.Managers
{
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public abstract class BasePlayerActionManager : IPlayerActionManager
    {
        private readonly ITrickState trickState;

        protected BasePlayerActionManager(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        public virtual bool ShouldManage(PlayerAction playerAction, Player player)
        {
            return playerAction != null && player.Position == trickState.PlayerTurn;
        }

        public abstract void Manage(PlayerAction playerAction, Player player);
    }
}