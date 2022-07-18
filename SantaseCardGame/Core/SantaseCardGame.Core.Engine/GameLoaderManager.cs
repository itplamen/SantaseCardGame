namespace SantaseCardGame.Core.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;
    
    public class GameLoaderManager : IGameLoaderManager
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly IStorage<Game> gameStorage;
        private readonly IStorage<Tuple<string, PlayerPosition, bool>> stateStorage;

        public GameLoaderManager(
            IGameState gameState, 
            IDeckState deckState, 
            ITrickState trickState, 
            IStorage<Game> gameStorage, 
            IStorage<Tuple<string, PlayerPosition, bool>> stateStorage)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.trickState = trickState;
            this.gameStorage = gameStorage;
            this.stateStorage = stateStorage;
        }

        public async Task SaveGame(Game game)
        {
            var state = new Tuple<string, PlayerPosition, bool>(game.Id, trickState.PlayerTurn, deckState.ShouldFollowSuit);

            await gameStorage.Add(game);
            await stateStorage.Add(state);
        }

        public async Task<IEnumerable<Game>> GetSavedGames() =>
            await gameStorage.GetAll();

        public async Task LoadGame(string id)
        {
            var games = await gameStorage.GetAll();
            var states = await stateStorage.GetAll();

            var game = games.FirstOrDefault(x => x.Id == id);
            var state = states.FirstOrDefault(x => x.Item1 == id);

            if (game == null || state == null)
            {
                throw new InvalidOperationException("Cannot load game!");
            }

            gameState.CurrentGameId = state.Item1;
            trickState.SetPlayerTurn(state.Item2);
            deckState.ShouldFollowSuit = state.Item3;

            await gameStorage.Add(game);
        }
    }
}
