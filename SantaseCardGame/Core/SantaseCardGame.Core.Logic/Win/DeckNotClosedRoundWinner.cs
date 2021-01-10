namespace SantaseCardGame.Core.Logic.Win
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class DeckNotClosedRoundWinner : BaseRoundWinner
    {
        private const int LAST_TRICK_WINNER_BONUS_POINTS = 10;

        private readonly IGameState gameState;
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public DeckNotClosedRoundWinner(IGameState gameState, IDeckState deckState, ITrickState trickState)
            : base(gameState)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public override Round GetWinner(IEnumerable<Player> players)
        {
            if (deckState.ClosedBy == PlayerPosition.NoOne && HasRoundEnded(players))
            {
                Player winner = players.FirstOrDefault(x => x.Points >= gameState.RoundWinPoints);

                if (winner != null)
                {
                    return GetRound(players, winner.Position);
                }
                else
                {
                    Player lastTrickWinner = players.First(x => x.Position == trickState.PlayerTurn);
                    lastTrickWinner.BonusPoints = LAST_TRICK_WINNER_BONUS_POINTS;

                    if (lastTrickWinner.Points >= gameState.RoundWinPoints)
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