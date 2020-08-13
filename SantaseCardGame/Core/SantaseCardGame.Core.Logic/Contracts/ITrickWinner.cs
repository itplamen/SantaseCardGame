namespace SantaseCardGame.Core.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ITrickWinner
    {
        PlayerPosition GetWinner(IEnumerable<Card> trickCards, CardSuit trumpSuit);
    }
}