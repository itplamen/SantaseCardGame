namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayFirstNotFollowingSuitStrategy : IPlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly IEnumerable<IPlaySpecialFeature> playSpecialFeatures;

        public PlayFirstNotFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlaySpecialFeature> playSpecialFeatures)
        {
            this.deckState = deckState;
            this.trickState = trickState;
            this.playSpecialFeatures = playSpecialFeatures;
        }

        public bool ShouldPlay(Player player)
        {
            return player.Position == PlayerPosition.First &&
                player.Position == trickState.PlayerTurn &&
                !deckState.ShouldFollowSuit;
        }

        public PlayerAction Play(Player player)
        {
            foreach (var feature in playSpecialFeatures)
            {
                PlayerAction playerAction = feature.Play(player);

                if (playerAction != null)
                {
                    return playerAction;
                }
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}