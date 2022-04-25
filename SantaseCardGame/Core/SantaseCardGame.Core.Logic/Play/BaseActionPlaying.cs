namespace SantaseCardGame.Core.Logic.Play
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public abstract class BaseActionPlaying : IActionPlaying
    {
        private readonly ITrickState trickState;

        protected BaseActionPlaying(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        protected virtual bool ShouldPlay(PlayerAction playerAction, Player player)
        {
            return playerAction != null &&
                player.Position == trickState.PlayerTurn &&
                // TODO: check if round was not won
                !trickState.Cards.Any(x => x.Key == player.Position);
        }

        public abstract PlayerActionResult Play(PlayerAction playerAction, Player player);
    }
}
