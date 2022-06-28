namespace SantaseCardGame.Data
{
    using System;

    using Microsoft.Extensions.Caching.Memory;

    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;

    public class InMemoryGameStorage : IGameStorage
    {
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions options;

        public InMemoryGameStorage(IMemoryCache memoryCache, int expiration)
        {
            this.memoryCache = memoryCache;
            this.options = new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(expiration) };
        }

        public void Add(Game game)
        {
            memoryCache.Set(game.Id, game, options);
        }

        public Game Get(string id)
        {
            return memoryCache.Get<Game>(id);
        }

        public void Update(Game game)
        {

        }

        public void Remove(string id)
        {
            memoryCache.Remove(id);
        }
    }
}
