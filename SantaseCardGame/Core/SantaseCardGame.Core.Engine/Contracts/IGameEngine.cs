namespace SantaseCardGame.Core.Engine.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IGameEngine
    {
        Game StartGame(string username);

        void ManageTrick(Game game);
    }
}