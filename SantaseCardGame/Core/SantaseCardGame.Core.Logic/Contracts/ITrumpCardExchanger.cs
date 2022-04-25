namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ITrumpCardExchanger
    {
        void Exchange(Card trumpCard, Deck deck, Player player);
    }
}
