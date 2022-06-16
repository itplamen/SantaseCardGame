namespace SantaseCardGame.Core.Logic.Contracts.Validators
{
    using SantaseCardGame.Data.Models;

    public interface ICardPlayableValidator
    {
        string Message { get; }

        bool CanPlay(Player player, Card card, Card opponentCard);
    }
}
