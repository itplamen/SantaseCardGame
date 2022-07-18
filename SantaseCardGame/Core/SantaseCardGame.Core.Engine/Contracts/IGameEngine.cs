namespace SantaseCardGame.Core.Engine.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models;

    public interface IGameEngine
    {
        Task<Game> CreateGame(GameType gameType, IEnumerable<string> players);

        Game LoadGame(string gameId);

        Task EndGame(string gameId);

        void ManageGame(Game game);

        void Play(PlayerAction playerAction, Player player);
    }
}
