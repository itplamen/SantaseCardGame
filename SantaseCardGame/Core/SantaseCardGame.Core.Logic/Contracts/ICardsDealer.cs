namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ICardsDealer
    {
        Deck Deal(Player firstPlayer, Player secondPlayer);

        void DrawCards(PlayerPosition winnerPosition, Game game);
    }
}