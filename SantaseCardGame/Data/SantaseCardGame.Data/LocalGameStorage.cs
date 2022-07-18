namespace SantaseCardGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.JSInterop;
    
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    
    public class LocalGameStorage : IGameStorage
    {
        private readonly IJSRuntime jsRuntime;
        private readonly IConfiguration configuration;

        public LocalGameStorage(IJSRuntime jsRuntime, IConfiguration configuration)
        {
            this.jsRuntime = jsRuntime;
            this.configuration = configuration;
        }

        public async Task Add(Game game)
        {
            var key = configuration["saveKey"];
            var games = await GetAll(key);
            games.Add(game);

            var jsonGames = JsonSerializer.Serialize(games);
            await jsRuntime.InvokeVoidAsync("saveGames", key, jsonGames);
        }

        public Game Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Game game)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(string id)
        {
            var key = configuration["saveKey"];
            var games = await GetAll(key);
            var game = games.FirstOrDefault(x => x.Id == id);

            if (game != null)
            {
                games.Remove(game);
            }

            var jsonGames = JsonSerializer.Serialize(games);
            await jsRuntime.InvokeVoidAsync("saveGames", key, jsonGames);
        }

        private async Task<IList<Game>> GetAll(string key)
        {
            var json = await jsRuntime.InvokeAsync<string>("getGames", key);

            if (!string.IsNullOrEmpty(json))
            {
                var games = JsonSerializer.Deserialize<IList<Game>>(json);

                return games;
            }

            return new List<Game>();
        }
    }
}
