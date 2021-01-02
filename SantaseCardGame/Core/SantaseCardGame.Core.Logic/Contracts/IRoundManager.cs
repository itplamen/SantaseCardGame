namespace SantaseCardGame.Core.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface IRoundManager
    {
        Deck StartRound(IEnumerable<Player> players);

        void EndRound(Round round, Game game);

        Round GetRoundWinner(IEnumerable<Player> players);

        PlayerPosition PlayTrick(Game game);
    }
}