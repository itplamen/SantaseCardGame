namespace SantaseCardGame.AI
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class GamePlayer : IGamePlayer
    {
        private readonly IEnumerable<IPlayerActionStrategy> strategies;

        public GamePlayer(IEnumerable<IPlayerActionStrategy> strategies)
        {
            this.strategies = strategies;
        }

        public PlayerAction Play(Player player)
        {
            foreach (var strategy in strategies)
            {
                PlayerAction playerAction = strategy.ChooseAction(player);

                if (playerAction.Type != PlayerActionType.None)
                {
                    return playerAction;
                }
            }

            throw new InvalidOperationException("AI player could not choose action!");
        }
    }
}
