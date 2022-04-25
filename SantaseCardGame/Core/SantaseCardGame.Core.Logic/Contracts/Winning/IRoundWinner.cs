namespace SantaseCardGame.Core.Logic.Contracts.Winning
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface IRoundWinner
    {
        Round GetWinner(PlayerPosition closedBy, IEnumerable<Player> players);
    }
}
