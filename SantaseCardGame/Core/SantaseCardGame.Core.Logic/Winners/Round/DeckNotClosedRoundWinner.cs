namespace SantaseCardGame.Core.Logic.Winners.Round
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class DeckNotClosedRoundWinner : BaseRoundWinner
    {
        private readonly IGameRules gameRules;
        private readonly ITrickState trickState;

        public DeckNotClosedRoundWinner(IGameRules gameRules, ITrickState trickState)
            : base(gameRules)
        {
            this.gameRules = gameRules;
            this.trickState = trickState;
        }

        public override Round GetWinner(PlayerPosition closedBy, IEnumerable<Player> players)
        {
            if (closedBy == PlayerPosition.None && HasRoundEnded(players))
            {
                Player winner = players.FirstOrDefault(x => x.Points >= gameRules.RoundWinPoints);

                if (winner != null)
                {
                    return GetRound(players, winner.Position);
                }
                else
                {
                    Player lastTrickWinner = players.First(x => x.Position == trickState.PlayerTurn);
                    lastTrickWinner.BonusPoints = gameRules.LastTrickWinnerBonusPoints;

                    if (lastTrickWinner.Points >= gameRules.RoundWinPoints)
                    {
                        return GetRound(players, lastTrickWinner.Position);
                    }
                }
            }

            return new Round();
        }

        private Round GetRound(IEnumerable<Player> players, PlayerPosition winnerPosition)
        {
            Player winner = players.First(x => x.Position == winnerPosition);
            Player loser = players.First(x => x.Position != winnerPosition);

            return new Round()
            {
                WinnerPosition = winner.Position,
                Points = GetWinnerPoints(loser)
            };
        }
    }
}
