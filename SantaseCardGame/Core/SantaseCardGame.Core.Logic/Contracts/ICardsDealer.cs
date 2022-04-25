namespace SantaseCardGame.Core.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ICardsDealer
    {
        Deck Deal(Player firstPlayer, Player secondPlayer);

        void DrawCards(Deck deck, IEnumerable<Player> players);
    }
}
