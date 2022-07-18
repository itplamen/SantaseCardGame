namespace SantaseCardGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Memory;

    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;

    public class InMemoryGameStorage : IGameStorage
    {
        private readonly IGameStorage gameStorage;
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions options;

        public InMemoryGameStorage(IGameStorage gameStorage, IMemoryCache memoryCache, int expiration)
        {
            this.gameStorage = gameStorage;
            this.memoryCache = memoryCache;
            this.options = new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(expiration) };
        }

        public async Task Add(Game game)
        {
            memoryCache.Set(game.Id, game, options);

            await gameStorage.Add(game);
        }

        public Game Get(string id)
        {
            return memoryCache.Get<Game>(id);
        }

        public async Task<IEnumerable<Game>> GetAll() =>
            await gameStorage.GetAll();

        public async Task Remove(string id)
        {
            memoryCache.Remove(id);

            await gameStorage.Remove(id);
        }
    }
}
