namespace SantaseCardGame.Core.Logic.Win
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class DeckClosedRoundWinner : BaseRoundWinner
    {
        private readonly IGameRules gameRules;
        private readonly IDeckState deckState;

        public DeckClosedRoundWinner(IGameRules gameRules, IDeckState deckState)
            : base(gameRules)
        {
            this.gameRules = gameRules;
            this.deckState = deckState;
        }

        public override Round GetWinner(Game game)
        {
            Round round = new Round();

            if (deckState.ClosedBy != PlayerPosition.NoOne)
            {
                Player winner = game.Players.First(x => x.Position == deckState.ClosedBy);
                Player loser = game.Players.First(x => x.Position != winner.Position);

                if (winner.Points >= gameRules.RoundWinPoints)
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