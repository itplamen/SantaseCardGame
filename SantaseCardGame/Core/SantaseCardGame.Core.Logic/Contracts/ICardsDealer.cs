namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ICardsDealer
    {
        void Deal(Deck deck, Player firstPlayer, Player secondPlayer);
    }
}