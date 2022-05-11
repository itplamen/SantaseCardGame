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

    public class GameEngine : IGameEngine
    {
        private readonly IGamePlayer gamePlayer;
        private readonly IInMemoryGameStorage gameStorage;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IEnumerable<IActionPlaying> actionsPlaying;
        private readonly IEnumerable<IGameStateHandler> gameStateHandlers;

        public GameEngine(
            IGamePlayer gamePlayer,
            IInMemoryGameStorage gameStorage, 
            IAnnouncementChecker announcementChecker, 
            IEnumerable<IActionPlaying> actionsPlaying, 
            IEnumerable<IGameStateHandler> gameStateHandlers)
        {
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
                Type = gameType
            };

            gameStorage.Add(game.Id, game);

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

        public void StartGame(Game game)
        {
            ManageGame(game);

            var botPlayer = game.Players.First();
            var playerAction = gamePlayer.Play(botPlayer);

            Play(playerAction, botPlayer);
        }

        public void EndGame(string gameId)
        {
            gameStorage.Remove(gameId);
        }

        public async void ManageGame(Game game)
        {
            if (!(game.Deck == null && game.Players.All(x => !x.Cards.Any())))
            {
                await SimulateThinking();
            }

            foreach (var stateHandler in gameStateHandlers)
            {
                stateHandler.Handle(game);
            }
        }

        public async void Play(PlayerAction playerAction, Player player)
        {
            if (playerAction.Type == PlayerActionType.PlayCard)
            {
                Announce announce = announcementChecker.GetAnnouncement(player, playerAction.Card);

                if (announce != Announce.None)
                {
                    playerAction.Announce = announce;
                }
            }

            if (player.Position == PlayerPosition.First)
            {
                await SimulateThinking();
            }
            
            foreach (var playAction in actionsPlaying)
            {
                playAction.Play(playerAction, player);
            }
        }

        private async Task SimulateThinking()
        {
            await Task.Delay(1500);
        }
    }
}
