namespace SantaseCardGame.AI.Logic.PlayLogics
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public abstract class BasePlayLogic : IPlayLogic
    {
        private readonly ITrickState trickState;
        private readonly IEnumerable<IPlayerActionStrategy> strategies;

        protected BasePlayLogic(ITrickState trickState, IEnumerable<IPlayerActionStrategy> strategies)
        {
            this.trickState = trickState;
            this.strategies = strategies;
        }

        public virtual bool ShouldPlay(Player player)
        {
            return player.Position == PlayerPosition.First && player.Position == trickState.PlayerTurn;
        }

        public IEnumerable<PlayerAction> Play(Player player)
        {
            var playerActions = new List<PlayerAction>();

            foreach (var strategy in strategies)
            {
                PlayerAction playerAction = strategy.ChooseAction(player);

                if (ShouldAddPlayerAction(playerAction, playerActions))
                {
                    playerActions.Add(playerAction);

                    if (trickState.Cards.Any())
                    {
                        break;
                    }
                }
            }

            return playerActions;
        }

        private bool ShouldAddPlayerAction(PlayerAction playerAction, IEnumerable<PlayerAction> playerActions)
        {
            if (playerAction.Type == PlayerActionType.PlayCard)
            {
                return !playerActions.Any(x => x.Type == PlayerActionType.Announce);
            }

            return playerAction.Type != PlayerActionType.None;
        }
    }
}
