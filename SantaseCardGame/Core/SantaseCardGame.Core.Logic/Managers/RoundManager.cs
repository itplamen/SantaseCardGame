namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class RoundManager : IRoundManager
    {
        private readonly IGameState gameState;
        private readonly ICardsDealer cardsDealer;
        private readonly IStatesManager statesManager;
        private readonly IEnumerable<IRoundWinner> roundWinners;

        public RoundManager(IGameState gameState, ICardsDealer cardsDealer, IStatesManager statesManager, IEnumerable<IRoundWinner> roundWinners)
        {
            this.gameState = gameState;
            this.cardsDealer = cardsDealer;
            this.statesManager = statesManager;
            this.roundWinners = roundWinners;
        }

        public Deck StartRound(IEnumerable<Player> players)
        {
            statesManager.ResetPlayers(players);
            PlayerPosition playerTurn = GetPlayerTurn();

            Deck deck = cardsDealer.Deal(players.First(), players.Last());
            statesManager.SetRoundStates(playerTurn, deck.Cards.Count, deck.TrumpCard);

            return deck;
        }

        public void EndRound(Round round, Game game)
        {
            gameState.RoundWinner = round.WinnerPosition;

            game.Rounds.Add(round);
            gameState.EndRound();
        }

        public Round GetRoundWinner(IEnumerable<Player> players)
        {
            foreach (var roundWinner in roundWinners)
            {
                Round round = roundWinner.GetWinner(players);

                if (round.WinnerPosition != PlayerPosition.NoOne)
                {
                    return round;
                }
            }

            return new Round();
        }

        private PlayerPosition GetPlayerTurn()
        {
            switch (gameState.RoundWinner)
            {
                case PlayerPosition.First:
                    return PlayerPosition.Second;
                case PlayerPosition.Second:
                    return PlayerPosition.First;
                default:
                    return PlayerPosition.First;
            }
        }
    }
}