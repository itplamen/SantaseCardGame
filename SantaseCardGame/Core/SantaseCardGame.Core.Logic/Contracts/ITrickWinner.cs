namespace SantaseCardGame.Core.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ITrickWinner
    {
        PlayerPosition GetWinner(IEnumerable<KeyValuePair<PlayerPosition, Card>> cards, CardSuit trumpSuit);
    }
}