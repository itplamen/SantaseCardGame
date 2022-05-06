namespace SantaseCardGame.Core.Engine.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IGameEngine
    {
        Game CreateGame(GameType gameType);

        void JoinGame(string gameId, string username);

        void StartGame(Game game);

        void ManageGamePlayer(Player player);

        void EndGame(string gameId);

        void Play(PlayerAction playerAction, Player player);
    }
}
