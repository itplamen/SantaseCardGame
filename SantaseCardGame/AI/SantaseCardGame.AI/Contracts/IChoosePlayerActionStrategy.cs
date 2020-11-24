namespace SantaseCardGame.AI.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IChoosePlayerActionStrategy
    {
        PlayerAction ChoosePlayerAction(Player player);
    }
}