namespace SantaseCardGame.Core.Logic.Win
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class DeckNotClosedRoundWinner : BaseRoundWinner
    {
        private readonly IGameRules gameRules;
        private readonly IDeckState deckState;

        public DeckNotClosedRoundWinner(IGameRules gameRules, IDeckState deckState)
            : base(gameRules)
        {
            this.gameRules = gameRules;
            this.deckState = deckState;
        }

        public override Round GetWinner(Game game)
        {
            Round round = new Round();

            if (deckState.ClosedBy == PlayerPosition.NoOne)
            {
                Player winner = game.Players.FirstOrDefault(x => x.Points >= gameRules.RoundWinPoints);

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