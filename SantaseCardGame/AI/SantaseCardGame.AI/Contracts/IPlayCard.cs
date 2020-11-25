namespace SantaseCardGame.AI.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayCard
    {
        Card PlayCard(Player player, Card opponentCard);
    }
}