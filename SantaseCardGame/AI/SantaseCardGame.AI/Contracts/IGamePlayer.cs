namespace SantaseCardGame.AI.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IGamePlayer
    {
        PlayerAction Play(Player player);
    }
}
