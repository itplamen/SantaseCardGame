namespace SantaseCardGame.Core.Logic.Winning.Round
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class DeckClosedRoundWinner : BaseRoundWinner
    {
        private readonly IGameRules gameRules;

        public DeckClosedRoundWinner(IGameRules gameRules)
            : base(gameRules)
        {
            this.gameRules = gameRules;
        }

        public override Round GetWinner(PlayerPosition closedBy, IEnumerable<Player> players)
        {
            Round round = new Round();

            if (closedBy != PlayerPosition.None && HasRoundEnded(players))
            {
                Player winner = players.First(x => x.Position == closedBy);
                Player loser = players.First(x => x.Position != winner.Position);

                if (winner.Points >= gameRules.RoundWinPoints)
                {
                    round.WinnerPosition = winner.Position;
                    round.Points = GetWinnerPoints(loser);
                }
                else
                {
                    round.WinnerPosition = loser.Position;
                    round.Points = loser.Hands.Any() ? gameRules.PlayerWinHalfRoundPoints : gameRules.PlayerWinMaxRoundPoints;
                }
            }

            return round;
        }
    }
}
