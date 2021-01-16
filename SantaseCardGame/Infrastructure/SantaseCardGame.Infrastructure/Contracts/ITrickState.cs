namespace SantaseCardGame.Infrastructure.Contracts
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ITrickState
    {
        event Action OnPlay;

        event Action OnDisplay;

        event Action OnGamePlayerTurn;

        PlayerPosition PlayerTurn { get; set; }

        IDictionary<PlayerPosition, Card> Cards { get; }

        void AddCard(Card card, PlayerPosition playerPosition);
    }
}