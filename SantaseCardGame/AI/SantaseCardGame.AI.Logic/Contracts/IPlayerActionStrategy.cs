namespace SantaseCardGame.AI.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayerActionStrategy
    {
        PlayerAction ChooseAction(Player player);
    }
}