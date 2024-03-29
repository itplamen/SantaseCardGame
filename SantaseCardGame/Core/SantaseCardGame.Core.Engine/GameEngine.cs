﻿namespace SantaseCardGame.Core.Engine
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
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly ITrickState trickState;
        private readonly IGamePlayer gamePlayer;
        private readonly IStorage<Game> gameStorage;
        private readonly IStorage<State> stateStorage;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IEnumerable<IActionPlaying> actionsPlaying;
        private readonly IEnumerable<IGameStateHandler> gameStateHandlers;

        public GameEngine(
            IDeckState deckState,
            IGameState gameState,
            ITrickState trickState,
            IGamePlayer gamePlayer,
            IStorage<Game> gameStorage,
            IStorage<State> stateStorage,
            IAnnouncementChecker announcementChecker, 
            IEnumerable<IActionPlaying> actionsPlaying, 
            IEnumerable<IGameStateHandler> gameStateHandlers)
        {
            this.deckState = deckState;
            this.gameState = gameState;
            this.trickState = trickState;
            this.gamePlayer = gamePlayer;
            this.gameStorage = gameStorage;
            this.stateStorage = stateStorage;
            this.announcementChecker = announcementChecker;
            this.actionsPlaying = actionsPlaying;
            this.gameStateHandlers = gameStateHandlers;
        }

        public async Task<Game> CreateGame(GameType gameType, IEnumerable<string> players)
        {
            var game = new Game()
            {
                Id = Guid.NewGuid().ToString(),
                Type = gameType
            };

            foreach (var username in players)
            {
                var player = new Player()
                {
                    Username = username,
                    Position = !game.Players.Any() ? PlayerPosition.First : PlayerPosition.Second
                };

                game.Players.Add(player);
            }

            gameState.CurrentGameId = game.Id;

            await gameStorage.Add(game);
            await stateStorage.Add(new State() { Id = game.Id });

            return game;
        }

        public Game GetCurrentGame() =>
            gameStorage.Get(gameState.CurrentGameId);

        public async Task EndGame(string gameId, bool removePermanentlySaved)
        {
            trickState.Clear();
            gameState.Clear();
            deckState.Clear();

            await gameStorage.Remove(gameId, removePermanentlySaved);
            await stateStorage.Remove(gameId, removePermanentlySaved);
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

        private async Task SimulateThinking() =>
            await Task.Delay(1500);
    }
}
