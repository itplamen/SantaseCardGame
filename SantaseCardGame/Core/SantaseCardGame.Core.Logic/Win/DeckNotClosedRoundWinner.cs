namespace SantaseCardGame.Core.Logic.Win
{
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

        public override Round GetWinner(Game game)
        {
            Round round = new Round();

            if (deckState.ClosedBy == PlayerPosition.NoOne)
            {
                Player winner = game.Players.FirstOrDefault(x => x.Points >= gameState.RoundWinPoints);

                if (winner != null)
                {
                    Player loser = game.Players.First(x => x.Position != winner.Position);
                    round.WinnerPosition = winner.Position;
                    round.Points = GetWinnerPoints(loser);
                }
            }

            return round;
        }
    }
}