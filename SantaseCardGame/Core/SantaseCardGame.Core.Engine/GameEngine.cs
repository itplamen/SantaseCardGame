namespace SantaseCardGame.Core.Engine
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class GameEngine : IGameEngine
    {
        private readonly ITrickManager trickManager;
        private readonly IRoundManager roundManager;
        private readonly ICardsDrawingManager cardsDrawingManager;

        public GameEngine(ITrickManager trickManager, IRoundManager roundManager, ICardsDrawingManager cardsDrawingManager)
        {
            this.trickManager = trickManager;
            this.roundManager = roundManager;
            this.cardsDrawingManager = cardsDrawingManager;
        }

        public Game StartGame(string username)
        {
            var players = new List<Player>()
            {
                new Player() 
                {
                    Username = "Bot",
                    Position = PlayerPosition.First
                },
                new Player()
                {
                    Username = username,
                    Position = PlayerPosition.Second
                }
            };

            Deck deck = roundManager.StartRound(players);

            return new Game()
            {
                Deck = deck,
                Players = players
            };
        }

        public void ManageGame(Game game)
        {
            PlayerPosition winnerPosition = trickManager.Play(game);
            Round round = roundManager.GetRoundWinner(game.Players);

            if (round.WinnerPosition == PlayerPosition.NoOne && game.Players.All(x => x.Cards.Any()))
            {
                cardsDrawingManager.DrawCards(winnerPosition, game);
            }
            else
            {
                roundManager.EndRound(round, game);
            }
        }
    }
}