namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlaySecondFollowingSuitStrategy : IPlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly IEnumerable<IPlayLogic> playLogics;

        public PlaySecondFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
        {
            this.deckState = deckState;
            this.trickState = trickState;
            this.playLogics = playLogics;
        }

        public bool ShouldPlay(Player player)
        {
            return player.Position == PlayerPosition.Second &&
                player.Position == trickState.PlayerTurn &&
                deckState.ShouldFollowSuit;
        }

        public PlayerAction Play(Player player)
        {
            foreach (var logic in playLogics)
            {
                PlayerAction playerAction = logic.Play(player);

                if (playerAction.Type != PlayerActionType.None)
                {
                    return playerAction;
                }
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}