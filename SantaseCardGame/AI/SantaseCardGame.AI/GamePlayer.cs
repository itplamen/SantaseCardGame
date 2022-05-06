namespace SantaseCardGame.AI
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        public async Task<PlayerAction> Play(Player player)
        {
            await SimulateThinking();

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

        private async Task SimulateThinking()
        {
            await Task.Delay(1500);
        }
    }
}
