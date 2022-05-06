namespace SantaseCardGame.Infrastructure.States.Contracts
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ITrickState
    {
        PlayerPosition PlayerTurn { get; }

        IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards { get; }

        event Action OnDisplay;

        void SetPlayerTurn(PlayerPosition playerPosition);

        void AddCard(Card card, PlayerPosition playerPosition);

        void Clear();
    }
}
