namespace SantaseCardGame.AI.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface IPlayLogic
    {
        bool ShouldPlay(Player player);

        IEnumerable<PlayerAction> Play(Player player);
    }
}
