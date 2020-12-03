namespace SantaseCardGame.AI.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface IPlayerActionStrategy
    {
        bool ShouldPlay(Player player);

        IEnumerable<PlayerAction> Play(Player player);
    }
}