namespace SantaseCardGame.Data.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ICardsProvider
    {
        IEnumerable<Card> Get();
    }
}
