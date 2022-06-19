namespace SantaseCardGame.Core.Logic.Play
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public abstract class BaseActionPlaying : IActionPlaying
    {
        private readonly IGameState gameState;
        private readonly ITrickState trickState;

        protected BaseActionPlaying(IGameState gameState, ITrickState trickState)
        {
            this.gameState = gameState;
            this.trickState = trickState;
        }

        protected virtual bool ShouldPlay(PlayerAction playerAction, Player player)
        {
            return playerAction != null &&
                player.Position == trickState.PlayerTurn &&
                gameState.RoundWinner == PlayerPosition.None &&
                !trickState.Cards.Any(x => x.Key == player.Position);
        }

        public abstract PlayerActionResult Play(PlayerAction playerAction, Player player);
    }
}
