namespace SantaseCardGame.Core.Logic.Winning.Round
{
    using System.Collections.Generic;
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

        public abstract Round GetWinner(PlayerPosition closedBy, IEnumerable<Player> players);

        protected bool HasRoundEnded(IEnumerable<Player> players)
        {
            return players.Any(x => x.Points >= gameRules.RoundWinPoints) || players.All(x => !x.Cards.Any());
        }

        protected int GetWinnerPoints(Player loser)
        {
            if (loser.Points >= gameRules.RoundHalfPoints)
            {
                return gameRules.PlayerWinMinRoundPoints;
            }

            if (loser.Hands.Any() && loser.Points < gameRules.RoundHalfPoints)
            {
                return gameRules.PlayerWinHalfRoundPoints;
            }

            return gameRules.PlayerWinMaxRoundPoints;
        }
    }
}