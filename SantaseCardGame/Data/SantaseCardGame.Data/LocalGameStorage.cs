namespace SantaseCardGame.Data
{
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
            var games = await GetAll();
            games.ToList().Add(game);

            await SaveGames(games);
        }

        public Game Get(string id)
        {
            var games = GetAll().Result;

            return games.FirstOrDefault(game => game.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            var key = configuration["gameKey"];
            var json = await jsRuntime.InvokeAsync<string>("getGames", key);

            if (!string.IsNullOrEmpty(json))
            {
                var games = JsonSerializer.Deserialize<IEnumerable<Game>>(json);

                return games;
            }

            return new List<Game>();
        }

        public async Task Remove(string id)
        {
            var games = await GetAll();
            var game = games.FirstOrDefault(x => x.Id == id);

            if (game != null)
            {
                games.ToList().Remove(game);
            }

            await SaveGames(games);
        }

        private async Task SaveGames(IEnumerable<Game> games)
        {
            var key = configuration["gameKey"];
            var jsonGames = JsonSerializer.Serialize(games);

            await jsRuntime.InvokeVoidAsync("saveGames", key, jsonGames);
        }
    }
}
