namespace SantaseCardGame.Infrastructure.States.Contracts
{
    using System;

    using SantaseCardGame.Data.Models;
    
    public interface IDeckState : IApplicationState
    {
        bool ShouldFollowSuit { get; set; }

        event Action<PlayerAction> OnCloseDeck;

        event Action<PlayerAction> OnChangeTrumpCard;

        event Action<Card> OnExchangeTrumpCardForNineOfTrumps;

        void CloseDeck();

        void ChangeTrumpCard(PlayerAction playerAction);

        void ExchangeTrumpCardForNineOfTrumps(Card nineOfTrumpsCard);
    }
}
