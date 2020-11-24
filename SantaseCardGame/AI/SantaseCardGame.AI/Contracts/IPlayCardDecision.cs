namespace SantaseCardGame.AI.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayCardDecision
    {
        Card PlayCard(Player player, Card opponentCard);
    }
}