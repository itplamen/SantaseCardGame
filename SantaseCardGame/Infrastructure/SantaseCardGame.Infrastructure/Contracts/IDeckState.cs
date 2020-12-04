namespace SantaseCardGame.Infrastructure.Contracts
{
    using System;

    using SantaseCardGame.Data.Models;

    public interface IDeckState
    {
        event Action<PlayerAction> OnClose;
        
        event Action<PlayerAction> OnChangeTrumpCard;

        event Action<Card> OnExchangeTrumpCardForNineOfTrumps;

        PlayerPosition ClosedBy { get; set; }

        Card TrumpCard { get; set; }

        int CardsLeft { get; set; }

        bool ShouldFollowSuit { get; set; }

        void Close();

        void ChangeTrumpCard(PlayerAction playerAction);

        void ExchangeTrumpCardForNineOfTrumps(Card nineOfTrumpsCard);
    }
}