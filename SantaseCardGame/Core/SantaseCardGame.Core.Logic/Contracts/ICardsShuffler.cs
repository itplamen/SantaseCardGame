namespace SantaseCardGame.Core.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ICardsShuffler
    {
        Deck Shuffle(IEnumerable<Card> cards);
    }
}