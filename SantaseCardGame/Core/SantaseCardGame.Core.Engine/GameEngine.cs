namespace SantaseCardGame.Core.Engine
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class GameEngine : IGameEngine
    {
        private readonly IGamePlayer gamePlayer;
        private readonly ITrickState trickState;
        private readonly IRoundManager roundManager;
        private readonly ICardsDrawingManager cardsDrawingManager;

        public GameEngine(IGamePlayer gamePlayer, ITrickState trickState, IRoundManager roundManager, ICardsDrawingManager cardsDrawingManager)
        {
            this.gamePlayer = gamePlayer;
            this.trickState = trickState;
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

        public async void ManagePlayersTurn(Game game)
        {
            if (trickState.PlayerTurn == PlayerPosition.First)
            {
                await Task.Delay(1500);
                gamePlayer.Play(game.Players.First());
            }
        }

        public void PlayTrick(Game game)
        {
            PlayerPosition winnerPosition = roundManager.PlayTrick(game);
            Round round = roundManager.GetRoundWinner(game.Players);

            if (round.WinnerPosition == PlayerPosition.NoOne)
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