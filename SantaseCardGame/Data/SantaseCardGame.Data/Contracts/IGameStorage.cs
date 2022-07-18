namespace SantaseCardGame.Data.Contracts
{
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models;
    
    public interface IGameStorage
    {
        Task Add(Game game);

        Game Get(string id);

        void Update(Game game);

        Task Remove(string id);
    }
}
