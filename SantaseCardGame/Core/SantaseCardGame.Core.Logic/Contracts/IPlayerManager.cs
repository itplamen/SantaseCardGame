namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayerManager
    {
        bool ChangeTrumpCard(Player player, Card trumpCard);

        bool CloseDeck(Player player);

        Announce PlayCard(Player player, Card card);
    }
}