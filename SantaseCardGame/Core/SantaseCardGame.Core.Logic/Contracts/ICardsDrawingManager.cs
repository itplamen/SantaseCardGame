namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ICardsDrawingManager
    {
        void DrawCards(PlayerPosition winnerPosition, Game game);
    }
}