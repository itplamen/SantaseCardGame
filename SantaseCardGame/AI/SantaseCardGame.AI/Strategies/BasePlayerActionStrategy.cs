namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public abstract class BasePlayerActionStrategy : IPlayerActionStrategy
    {
        private readonly ITrickState trickState;
        private readonly IEnumerable<IPlayLogic> playLogics;

        protected BasePlayerActionStrategy(ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
        {
            this.trickState = trickState;
            this.playLogics = playLogics;
        }

        public virtual bool ShouldPlay(Player player)
        {
            return player.Position == PlayerPosition.First && player.Position == trickState.PlayerTurn;
        }

        public IEnumerable<PlayerAction> Play(Player player)
        {
            var playerActions = new List<PlayerAction>();

            foreach (var logic in playLogics)
            {
                PlayerAction playerAction = logic.Play(player);

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

            // TODO: Add decorator and validate throw new InvalidOperationException("AI did not play any strategy!");
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