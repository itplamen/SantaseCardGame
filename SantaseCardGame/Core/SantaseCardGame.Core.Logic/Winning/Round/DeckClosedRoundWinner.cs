namespace SantaseCardGame.Core.Logic.Winning.Round
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class DeckClosedRoundWinner : BaseRoundWinner
    {
        private readonly IGameState gameState;

        public DeckClosedRoundWinner(IGameState gameState)
            : base(gameState)
        {
            this.gameState = gameState;
        }

        public override (PlayerPosition position, int points) GetWinner(PlayerPosition closedBy, IEnumerable<Player> players)
        {
            PlayerPosition winnerPosition = PlayerPosition.None;
            int points = 0;

            if (closedBy != PlayerPosition.None && HasRoundEnded(players))
            {
                Player winner = players.First(x => x.Position == closedBy);
                Player loser = players.First(x => x.Position != winner.Position);

                if (winner.Points >= gameState.RoundWinPoints)
                {
                    winnerPosition = winner.Position;
                    points = CalculateWinnerPoints(loser);
                }
                else
                {
                    winnerPosition = loser.Position;
                    points = loser.Hands.Any() ? gameState.PlayerWinHalfRoundPoints : gameState.PlayerWinMaxRoundPoints;
                }
            }

            return (winnerPosition, points);
        }
    }
}
