namespace SantaseCardGame.Core.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface IStatesManager
    {
        void ResetStates();

        void SetRoundStates(PlayerPosition playerTurn, int cardsLeft, Card trump);

        void ResetPlayers(IEnumerable<Player> players);
    }
}