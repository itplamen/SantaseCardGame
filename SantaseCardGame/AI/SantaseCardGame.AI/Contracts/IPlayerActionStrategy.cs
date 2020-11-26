namespace SantaseCardGame.AI.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayerActionStrategy
    {
        bool ShouldPlay(Player player);

        PlayerAction Play(Player player);
    }
}