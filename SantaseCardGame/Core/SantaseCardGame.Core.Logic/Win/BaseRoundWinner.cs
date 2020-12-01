namespace SantaseCardGame.Core.Logic.Win
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public abstract class BaseRoundWinner : IRoundWinner
    {
        private readonly IGameRules gameRules;

        protected BaseRoundWinner(IGameRules gameRules)
        {
            this.gameRules = gameRules;
        }

        public abstract Round GetWinner(Game game);

        protected int GetWinnerPoints(Player loser)
        {
            if (loser.Points >= gameRules.RoundHalfPoints)
            {
                return 1;
            }
            
            if (loser.Hands.Any() && loser.Points < gameRules.RoundHalfPoints)
            {
                return 2;
            }

            return 3;
        }
    }
}