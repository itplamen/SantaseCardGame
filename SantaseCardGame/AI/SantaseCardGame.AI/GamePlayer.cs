namespace SantaseCardGame.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async void Play(Player player)
        {
            IPlayerActionStrategy strategy = strategies.FirstOrDefault(x => x.ShouldPlay(player));

            if (strategy != null)
            {
                IEnumerable<PlayerAction> playerActions = strategy.Play(player);

                foreach (var playerAction in playerActions)
                {
                    await Task.Delay(1500);

                    IPlayerActionManager manager = managers.First(x => x.ShouldManage(playerAction, player));
                    manager.Manage(playerAction, player);
                }
            }
        }
    }
}