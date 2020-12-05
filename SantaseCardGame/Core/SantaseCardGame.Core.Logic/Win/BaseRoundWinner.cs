namespace SantaseCardGame.Core.Logic.Win
{
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

        public abstract Round GetWinner(Game game);

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