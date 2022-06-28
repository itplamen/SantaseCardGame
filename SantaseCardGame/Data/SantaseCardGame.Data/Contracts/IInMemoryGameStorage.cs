using SantaseCardGame.Data.Models;

namespace SantaseCardGame.Data.Contracts
{
    public interface IGameStorage
    {
        void Add(string id, Game game);

        Game Get(string id);

        void Remove(string id);
    }
}
