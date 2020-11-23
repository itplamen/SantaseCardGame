namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IAnnounceCardProvider
    {
        PlayerAction GetAnnounce(Player player, Card card);
    }
}