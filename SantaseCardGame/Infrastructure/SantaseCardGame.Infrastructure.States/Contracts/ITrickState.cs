namespace SantaseCardGame.Infrastructure.States.Contracts
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ITrickState : IApplicationState
    {
        PlayerPosition PlayerTurn { get; }

        IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards { get; }

        event Action OnDisplay;

        event Action OnManage;

        void SetPlayerTurn(PlayerPosition playerPosition);

        void AddCard(Card card, PlayerPosition playerPosition);

        void Display();
    }
}
