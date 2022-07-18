namespace SantaseCardGame.Core.Engine.Contracts
{
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models;

    public interface IGameEngine
    {
        Task<Game> CreateGame(GameType gameType);

        void JoinGame(string gameId, string username);

        Game LoadGame(string gameId);

        Task EndGame(string gameId);

        void ManageGame(Game game);

        void Play(PlayerAction playerAction, Player player);
    }
}
