namespace SantaseCardGame.Core.Logic.Win
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class DeckNotClosedRoundWinner : BaseRoundWinner
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;

        public DeckNotClosedRoundWinner(IGameState gameState, IDeckState deckState)
            : base(gameState)
        {
            this.gameState = gameState;
            this.deckState = deckState;
        }

        public override Round GetWinner(IEnumerable<Player> players)
        {
            Round round = new Round();

            if (AreRoundWinPointsReached(players) && deckState.ClosedBy == PlayerPosition.NoOne)
            {
                Player winner = players.FirstOrDefault(x => x.Points >= gameState.RoundWinPoints);

                if (winner != null)
                {
                    Player loser = players.First(x => x.Position != winner.Position);
                    round.WinnerPosition = winner.Position;
                    round.Points = GetWinnerPoints(loser);
                }
            }

            return round;
        }
    }
}