namespace SantaseCardGame.Core.Logic.Win
{
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

        public override Round GetWinner(Game game)
        {
            Round round = new Round();

            if (AreRoundWinPointsReached(game) && deckState.ClosedBy != PlayerPosition.NoOne)
            {
                Player winner = game.Players.First(x => x.Position == deckState.ClosedBy);
                Player loser = game.Players.First(x => x.Position != winner.Position);

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