namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IGameStateHandler
    {
        void Handle(Game game);
    }
}
