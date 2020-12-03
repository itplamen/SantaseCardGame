namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;

    public abstract class BasePlayerActionStrategy : IPlayerActionStrategy
    {
        private readonly IEnumerable<IPlayLogic> playLogics;

        protected BasePlayerActionStrategy(IEnumerable<IPlayLogic> playLogics)
        {
            this.playLogics = playLogics;
        }

        public abstract bool ShouldPlay(Player player);

        public IEnumerable<PlayerAction> Play(Player player)
        {
            var playerActions = new List<PlayerAction>();

            foreach (var logic in playLogics)
            {
                PlayerAction playerAction = logic.Play(player);

                if (ShouldAddPlayerAction(playerAction, playerActions))
                {
                    playerActions.Add(playerAction);
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