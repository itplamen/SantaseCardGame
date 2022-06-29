namespace SantaseCardGame.Core.Logic.Contracts.Winning
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface IRoundWinner
    {
        (PlayerPosition position, int points) GetWinner(PlayerPosition closedBy, IEnumerable<Player> players);
    }
}
