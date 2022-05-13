namespace SantaseCardGame.Core.Logic.Winning.Round
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Winning;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public abstract class BaseRoundWinner : IRoundWinner
    {
        private readonly IGameState gameState;

        protected BaseRoundWinner(IGameState gameState)
        {
            this.gameState = gameState;
        }

        public abstract Round GetWinner(PlayerPosition closedBy, IEnumerable<Player> players);

        protected bool HasRoundEnded(IEnumerable<Player> players)
        {
            return players.Any(x => x.Hands.Any()) &&
                (players.Any(x => x.Points >= gameState.RoundWinPoints) || 
                players.All(x => !x.Cards.Any()));
        }

        protected int GetWinnerPoints(Player loser)
        {
            if (loser.Points >= gameState.RoundHalfPoints)
            {
                return gameState.PlayerWinMinRoundPoints;
            }

            if (loser.Hands.Any() && loser.Points < gameState.RoundHalfPoints)
            {
                return gameState.PlayerWinHalfRoundPoints;
            }

            return gameState.PlayerWinMaxRoundPoints;
        }
    }
}