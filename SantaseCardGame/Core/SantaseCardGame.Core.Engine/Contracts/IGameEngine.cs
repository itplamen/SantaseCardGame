namespace SantaseCardGame.Core.Engine.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IGameEngine
    {
        Game CreateGame(GameType gameType);

        void JoinGame(string gameId, string username);

        void EndGame(string gameId);

        void ManageGame(Game game);

        void Play(PlayerAction playerAction, Player player);
    }
}
