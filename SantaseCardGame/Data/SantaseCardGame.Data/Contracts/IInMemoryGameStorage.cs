using SantaseCardGame.Data.Models;

namespace SantaseCardGame.Data.Contracts
{
    public interface IInMemoryGameStorage
    {
        void Add(string id, Game game);

        Game Get(string id);

        Game GetByPlayerId(string playerId);

        void Remove(string id);
    }
}
