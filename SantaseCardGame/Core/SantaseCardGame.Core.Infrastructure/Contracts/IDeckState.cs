namespace SantaseCardGame.Core.Infrastructure.Contracts
{
    using System;

    using SantaseCardGame.Data.Models;

    public interface IDeckState
    {
        event Action OnClose;
        
        event Action<Card> OnChangeTrumpCard;

        event Action<Card> OnExchangeTrumpCardForNineOfTrumps;

        bool IsClosed { get; set; }

        bool ShouldFollowSuit { get; set; }

        void Close();

        void ChangeTrumpCard(Card trumpCard);

        void ExchangeTrumpCardForNineOfTrumps(Card nineOfTrumpsCard);
    }
}