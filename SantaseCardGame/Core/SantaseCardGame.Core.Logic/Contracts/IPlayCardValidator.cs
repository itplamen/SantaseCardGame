namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayCardValidator
    {
        string Message { get; }

        bool CanPlay(Player player, Card card, Card opponentCard);
    }
}