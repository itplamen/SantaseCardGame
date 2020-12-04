namespace SantaseCardGame.Infrastructure.Contracts
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ITrickState
    {
        event Action OnPlay;

        event Action OnClear;

        event Action OnDisplay;

        event Action OnManagePlayerTurn;

        PlayerPosition PlayerTurn { get; set; }

        IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards { get; }

        void AddCard(Card card, PlayerPosition playerPosition);

        void Clear();
    }
}