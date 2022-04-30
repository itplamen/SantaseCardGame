namespace SantaseCardGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.Caching.Memory;

    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;

    public class InMemoryGameStorage: IInMemoryGameStorage
    {
        private readonly List<string> gameIds;
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions options;

        public InMemoryGameStorage(IMemoryCache memoryCache, int expiration)
        {
            this.gameIds = new List<string>();
            this.memoryCache = memoryCache;
            this.options = new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expiration) };
        }

        public void Add(string id, Game game)
        {
            var addedGame = memoryCache.Set(id, game, options);

            if (addedGame != null)
            {
                gameIds.Add(id);
            }
        }

        public Game Get(string id)
        {
            return memoryCache.Get<Game>(id);
        }

        public Game GetByPlayerId(string playerId)
        {
            foreach (var gameId in gameIds)
            {
                Game game = (Game)memoryCache.Get(gameId);

                if (game != null && game.Players.Any(x => x.Id == playerId))
                {
                    return game;
                }
            }

            return null;
        }

        public void Remove(string id)
        {
            memoryCache.Remove(id);
        }
    }
}
