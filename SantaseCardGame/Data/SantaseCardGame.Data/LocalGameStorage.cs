namespace SantaseCardGame.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.JSInterop;
    
    using SantaseCardGame.Data.Models;
    
    public class LocalGameStorage : BaseStorage<Game>
    {
        public LocalGameStorage(IJSRuntime jsRuntime, IConfiguration configuration)
            : base(jsRuntime, configuration, "gameKey")
        {
        }

        public override async Task Remove(string id)
        {
            var games = await GetAll();
            var game = games.FirstOrDefault(x => x.Id == id);

            if (game != null)
            {
                games.ToList().Remove(game);
            }

            await Save(games);
        }
    }
}
