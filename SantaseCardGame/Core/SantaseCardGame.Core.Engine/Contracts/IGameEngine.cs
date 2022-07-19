namespace SantaseCardGame.Core.Engine.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models;

    public interface IGameEngine
    {
        Task<Game> CreateGame(GameType gameType, IEnumerable<string> players);

        Task EndGame(string gameId, bool removePermanentlySaved);

        Game GetCurrentGame();

        void ManageGame(Game game);

        void Play(PlayerAction playerAction, Player player);
    }
}
