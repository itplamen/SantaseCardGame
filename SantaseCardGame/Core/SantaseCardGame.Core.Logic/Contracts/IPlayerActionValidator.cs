namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayerActionValidator
    {
        bool CanAnnounce(Player player);
    }
}