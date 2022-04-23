namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ICardPlayableValidator
    {
        bool CanPlay(Player player, Card card, Card opponentCard);
    }
}
