namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IGameManager
    {
        Game StartGame(string username);
    }
}