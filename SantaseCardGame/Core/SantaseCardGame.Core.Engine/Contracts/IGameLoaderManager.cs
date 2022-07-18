namespace SantaseCardGame.Core.Engine.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models;

    public interface IGameLoaderManager
    {
        Task SaveGame(Game game);

        Task<IEnumerable<Game>> GetSavedGames();

        Task LoadGame(string id);
    }
}
