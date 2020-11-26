namespace SantaseCardGame.AI.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlaySpecialFeature
    {
        PlayerAction Play(Player player);
    }
}