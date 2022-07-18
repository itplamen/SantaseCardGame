namespace SantaseCardGame.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models;
    
    public interface IGameStorage
    {
        Task Add(Game game);

        Game Get(string id);

        Task<IEnumerable<Game>> GetAll();

        Task Remove(string id);
    }
}
