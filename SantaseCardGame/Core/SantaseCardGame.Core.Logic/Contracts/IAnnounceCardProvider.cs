namespace SantaseCardGame.Core.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface IAnnounceCardProvider
    {
        PlayerAction GetAnnounce(Player player, Card card);

        IEnumerable<Card> GetMarriages(Player player);

        CardType AnnounceCardTypeToSearch(Card card);
    }
}