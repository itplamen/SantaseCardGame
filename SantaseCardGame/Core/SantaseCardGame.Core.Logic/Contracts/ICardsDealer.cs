namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ICardsDealer
    {
        Deck Deal(Player firstPlayer, Player secondPlayer);
    }
}
