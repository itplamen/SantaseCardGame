namespace SantaseCardGame.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class GamePlayer : IGamePlayer
    {
        private readonly IEnumerable<IPlayerActionManager> managers;
        private readonly IEnumerable<IPlayerActionStrategy> strategies;

        public GamePlayer(IEnumerable<IPlayerActionManager> managers, IEnumerable<IPlayerActionStrategy> strategies)
        {
            this.managers = managers;
            this.strategies = strategies;
        }

        public void Play(Player player)
        {
            IPlayerActionStrategy strategy = strategies.FirstOrDefault(x => x.ShouldPlay(player));

            if (strategy != null)
            {
                PlayerAction playerAction = strategy.Play(player);
                managers.First(x => x.ShouldManage(playerAction, player)).Manage(playerAction, player);

                return;
            }

            throw new InvalidOperationException("AI did not play any strategy!");
        }
    }
}