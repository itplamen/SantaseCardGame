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

        public override Round GetWinner(PlayerPosition closedBy, IEnumerable<Player> players)
        {
            Round round = new Round();

            if (closedBy != PlayerPosition.None && HasRoundEnded(players))
            {
                Player winner = players.First(x => x.Position == closedBy);
                Player loser = players.First(x => x.Position != winner.Position);

                if (winner.Points >= gameState.RoundWinPoints)
                {
                    round.WinnerPosition = winner.Position;
                    round.Points = GetWinnerPoints(loser);
                }
                else
                {
                    round.WinnerPosition = loser.Position;
                    round.Points = loser.Hands.Any() ? gameState.PlayerWinHalfRoundPoints : gameState.PlayerWinMaxRoundPoints;
                }
            }

            return round;
        }
    }
}
