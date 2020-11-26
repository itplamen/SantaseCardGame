namespace SantaseCardGame.AI.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayLogic
    {
        PlayerAction Play(Player player);
    }
}