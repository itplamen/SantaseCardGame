namespace SantaseCardGame.Core.Logic.Contracts
{
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models;

    public interface IAnnouncementChecker
    {
        PlayerAction GetAnnouncement(Player player, Card card);

        IEnumerable<Card> GetMarriages(IEnumerable<Card> cards);

        CardType MarriageCardTypeToSearch(Card card);
    }
}
