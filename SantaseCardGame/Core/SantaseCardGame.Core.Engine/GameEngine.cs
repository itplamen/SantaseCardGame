namespace SantaseCardGame.Core.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class GameEngine : IGameEngine
    {
        private readonly IGameState gameState;
        private readonly ITrickState trickState;
        private readonly IGamePlayer gamePlayer;
        private readonly IGameStorage gameStorage;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IEnumerable<IActionPlaying> actionsPlaying;
        private readonly IEnumerable<IGameStateHandler> gameStateHandlers;

        public GameEngine(
            IGameState gameState,
            ITrickState trickState,
            IGamePlayer gamePlayer,
            IGameStorage gameStorage, 
            IAnnouncementChecker announcementChecker, 
            IEnumerable<IActionPlaying> actionsPlaying, 
            IEnumerable<IGameStateHandler> gameStateHandlers)
        {
            this.gameState = gameState;
            this.trickState = trickState;
            this.gamePlayer = gamePlayer;
            this.gameStorage = gameStorage;
            this.announcementChecker = announcementChecker;
            this.actionsPlaying = actionsPlaying;
            this.gameStateHandlers = gameStateHandlers;
        }

        public Game CreateGame(GameType gameType)
        {
            var game = new Game()
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateTime.UtcNow,
                Type = gameType
            };

            gameStorage.Add(game);
            gameState.CurrentGameId = game.Id;

            return game;
        }

        public void JoinGame(string gameId, string username)
        {
            var game = gameStorage.Get(gameId);

            var player = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Position = !game.Players.Any() ? PlayerPosition.First : PlayerPosition.Second
            };

            game.AddPlayer(player);
        }

        public Game LoadGame(string gameId) =>
            gameStorage.Get(gameId);

        public void EndGame(string gameId)
        {
            gameStorage.Remove(gameId);
            gameState.CurrentGameId = string.Empty;
        }

        public async void ManageGame(Game game)
        {
            if (trickState.Cards.Count() == gameState.TrickCardsCount)
            {
                await SimulateThinking();
            }

            foreach (var stateHandler in gameStateHandlers)
            {
                stateHandler.Handle(game);
            }

            trickState.Display();

            if (trickState.PlayerTurn == PlayerPosition.First && gameState.RoundWinner == PlayerPosition.None)
            {
                ManageGamePlayerTurn(game.Players.First());
            }
        }

        public void Play(PlayerAction playerAction, Player player)
        {
            if (playerAction.Type == PlayerActionType.PlayCard)
            {
                Announce announce = announcementChecker.GetAnnouncement(player, playerAction.Card);

                if (announce != Announce.None)
                {
                    playerAction.Announce = announce;
                    playerAction.Type = PlayerActionType.AnnounceCardMarriage;
                }
            }

            foreach (var playAction in actionsPlaying)
            {
                var result = playAction.Play(playerAction, player);

                if (!string.IsNullOrEmpty(result.Message))
                {
                    gameState.ShowMessage(player.Position, result.Message);
                }
            }
        }

        private async void ManageGamePlayerTurn(Player player)
        {
            var playerActions = gamePlayer.Play(player);

            foreach (var playerAction in playerActions)
            {
                await SimulateThinking();

                Play(playerAction, player);
            }
        }

        private async Task SimulateThinking()
        {
            await Task.Delay(1500);
        }
    }
}
