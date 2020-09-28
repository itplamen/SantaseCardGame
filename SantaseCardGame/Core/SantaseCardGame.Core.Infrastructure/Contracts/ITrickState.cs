namespace SantaseCardGame.Core.Infrastructure.Contracts
{
    using System;

    using SantaseCardGame.Data.Models;

    public interface ITrickState
    {
        event Action OnPlay;

        event Action OnClear;

        event Action OnDisplay;

        Card[] Cards { get; }

        PlayerPosition PlayerTurn { get; set; }

        CardSuit TrumpCardSuit { get; set; }

        void AddCard(Card card, PlayerPosition playerPosition);

        void Clear();
    }
}