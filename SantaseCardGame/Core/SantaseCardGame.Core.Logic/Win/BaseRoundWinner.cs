namespace SantaseCardGame.Core.Logic.Win
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public abstract class BaseRoundWinner : IRoundWinner
    {
        private readonly IGameState gameState;

        protected BaseRoundWinner(IGameState gameState)
        {
            this.gameState = gameState;
        }

        public abstract Round GetWinner(IEnumerable<Player> players);

        protected bool HasRoundEnded(IEnumerable<Player> players)
        {
            return players.Any(x => x.Points >= gameState.RoundWinPoints) || players.All(x => !x.Cards.Any());
        }

        protected int GetWinnerPoints(Player loser)
        {
            if (loser.Points >= gameState.RoundHalfPoints)
            {
                return 1;
            }
            
            if (loser.Hands.Any() && loser.Points < gameState.RoundHalfPoints)
            {
                return 2;
            }

            return 3;
        }
    }
}