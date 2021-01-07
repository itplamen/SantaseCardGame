namespace SantaseCardGame.Core.Logic.Managers
{
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public abstract class BasePlayerActionManager : IPlayerActionManager
    {
        private readonly IGameState gameState;
        private readonly ITrickState trickState;

        protected BasePlayerActionManager(IGameState gameState, ITrickState trickState)
        {
            this.gameState = gameState;
            this.trickState = trickState;
        }

        public virtual bool ShouldManage(PlayerAction playerAction, Player player)
        {
            return playerAction != null && 
                player.Position == trickState.PlayerTurn &&
                gameState.RoundWinner == PlayerPosition.NoOne;
        }

        public abstract void Manage(PlayerAction playerAction, Player player);
    }
}