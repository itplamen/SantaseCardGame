using SantaseCardGame.Data.Models;

namespace SantaseCardGame.Data.Contracts
{
    public interface IGameStorage
    {
        void Add(Game game);

        Game Get(string id);

        void Update(Game game);

        void Remove(string id);
    }
}
