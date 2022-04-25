namespace SantaseCardGame.Infrastructure.States.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface ITrickState
    {
        PlayerPosition PlayerTurn { get; }

        IEnumerable<KeyValuePair<PlayerPosition, Card>> Cards { get; }

        void SetPlayerTurn(PlayerPosition playerPosition);

        void AddCard(Card card, PlayerPosition playerPosition);
    }
}
