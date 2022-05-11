using SantaseCardGame.Data.Models;

namespace SantaseCardGame.Data.Contracts
{
    public interface IInMemoryGameStorage
    {
        void Add(string id, Game game);

        Game Get(string id);

        void Remove(string id);
    }
}
