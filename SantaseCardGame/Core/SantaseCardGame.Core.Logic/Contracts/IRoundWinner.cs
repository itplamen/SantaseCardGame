namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IRoundWinner
    {
        Round GetWinner(Game game);
    }
}