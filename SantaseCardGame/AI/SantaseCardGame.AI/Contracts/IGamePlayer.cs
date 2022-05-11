namespace SantaseCardGame.AI.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;
    
    public interface IGamePlayer
    {
        IEnumerable<PlayerAction> Play(Player player);
    }
}
