namespace SantaseCardGame.AI.Strategies
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;

    public abstract class BasePlayerActionStrategy : IPlayerActionStrategy
    {
        private readonly IEnumerable<IPlayLogic> playLogics;

        public BasePlayerActionStrategy(IEnumerable<IPlayLogic> playLogics)
        {
            this.playLogics = playLogics;
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

            throw new InvalidOperationException("AI did not play any strategy!");
        }

        public abstract bool ShouldPlay(Player player);
    }
}