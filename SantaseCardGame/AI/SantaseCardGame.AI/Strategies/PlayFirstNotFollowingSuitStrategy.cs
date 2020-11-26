namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayFirstNotFollowingSuitStrategy : IChoosePlayerActionStrategy
    {
        private readonly IEnumerable<IPlaySpecialFeature> playSpecialFeatures;

        public PlayFirstNotFollowingSuitStrategy(IEnumerable<IPlaySpecialFeature> playSpecialFeatures)
        {
            this.playSpecialFeatures = playSpecialFeatures;
        }

        public PlayerAction ChoosePlayerAction(Player player)
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