namespace SantaseCardGame.Core.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;

    public class GameEngine : IGameEngine
    {
        private readonly IInMemoryGameStorage gameStorage;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IEnumerable<IActionPlaying> actionsPlaying;
        private readonly IEnumerable<IGameStateHandler> gameStateHandlers;

        public GameEngine(
            IInMemoryGameStorage gameStorage, 
            IAnnouncementChecker announcementChecker, 
            IEnumerable<IActionPlaying> actionsPlaying, 
            IEnumerable<IGameStateHandler> gameStateHandlers)
        {
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

        public void EndGame(string gameId)
        {
            gameStorage.Remove(gameId);
        }

        public void Play(PlayerAction playerAction, Player player)
        {
            if (playerAction.Type == PlayerActionType.PlayCard)
            {
                Announce announce = announcementChecker.GetAnnouncement(player, playerAction.Card);

                if (announce != Announce.None)
                {
                    playerAction.Announce = announce;
                }
            }

            actionsPlaying.ToList().ForEach(x => x.Play(playerAction, player));

            Game game = gameStorage.GetByPlayerId(player.Id);
            gameStateHandlers.ToList().ForEach(x => x.Handle(game));
        }
    }
}
