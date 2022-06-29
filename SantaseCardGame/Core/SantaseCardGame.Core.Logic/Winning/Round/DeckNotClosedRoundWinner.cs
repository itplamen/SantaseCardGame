namespace SantaseCardGame.Core.Logic.Winning.Round
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class DeckNotClosedRoundWinner : BaseRoundWinner
    {
        private readonly IGameState gameState;
        private readonly ITrickState trickState;

        public DeckNotClosedRoundWinner(IGameState gameState, ITrickState trickState)
            : base(gameState)
        {
            this.gameState = gameState;
            this.trickState = trickState;
        }

        public override (PlayerPosition position, int points) GetWinner(PlayerPosition closedBy, IEnumerable<Player> players)
        {
            PlayerPosition winnerPosition = PlayerPosition.None;
            int points = 0;

            if (closedBy == PlayerPosition.None && HasRoundEnded(players))
            {
                Player winner = players.FirstOrDefault(x => x.Points >= gameState.RoundWinPoints);

                if (winner != null)
                {
                    Player loser = players.First(x => x.Position != winner.Position);
                    winnerPosition = winner.Position;
                    points = CalculateWinnerPoints(loser);
                }
                else
                {
                    Player lastTrickWinner = players.First(x => x.Position == trickState.PlayerTurn);
                    lastTrickWinner.BonusPoints = gameState.LastTrickWinnerBonusPoints;

                    if (lastTrickWinner.Points >= gameState.RoundWinPoints)
                    {
                        Player loser = players.First(X => X.Position != lastTrickWinner.Position);
                        winnerPosition = lastTrickWinner.Position;
                        points = CalculateWinnerPoints(loser);
                    }
                }
            }

            return (winnerPosition, points);
        }
    }
}
