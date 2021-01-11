namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ITrickManager
    {
        PlayerPosition Play(Game game);
    }
}
