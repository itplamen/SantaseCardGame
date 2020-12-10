namespace SantaseCardGame.Core.Logic.Win
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class DeckClosedRoundWinner : BaseRoundWinner
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;

        public DeckClosedRoundWinner(IGameState gameState, IDeckState deckState)
            : base(gameState)
        {
            this.gameState = gameState;
            this.deckState = deckState;
        }

        public override Round GetWinner(IEnumerable<Player> players)
        {
            Round round = new Round();

            if (AreRoundWinPointsReached(players) && deckState.ClosedBy != PlayerPosition.NoOne)
            {
                Player winner = players.First(x => x.Position == deckState.ClosedBy);
                Player loser = players.First(x => x.Position != winner.Position);

                if (winner.Points >= gameState.RoundWinPoints)
                {
                    round.WinnerPosition = winner.Position;
                    round.Points = GetWinnerPoints(loser);
                }
                else
                {
                    round.WinnerPosition = loser.Position;

                    if (loser.Hands.Any())
                    {
                        round.Points = 2;
                    }
                    else
                    {
                        round.Points = 3;
                    }
                }
            }

            return round;
        }
    }
}