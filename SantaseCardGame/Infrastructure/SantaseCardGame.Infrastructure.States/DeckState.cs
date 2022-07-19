namespace SantaseCardGame.Infrastructure.States
{
    using System;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class DeckState : IDeckState
    {
        public bool ShouldFollowSuit { get; set; }

        public event Action<PlayerAction> OnCloseDeck;

        public event Action<PlayerAction> OnChangeTrumpCard;

        public event Action<Card> OnExchangeTrumpCardForNineOfTrumps;

        public void CloseDeck() =>
            OnCloseDeck?.Invoke(new PlayerAction(PlayerActionType.CloseDeck));

        public void ChangeTrumpCard(PlayerAction playerAction) =>
            OnChangeTrumpCard?.Invoke(playerAction);

        public void ExchangeTrumpCardForNineOfTrumps(Card nineOfTrumpsCard) => 
            OnExchangeTrumpCardForNineOfTrumps?.Invoke(nineOfTrumpsCard);

        public void Clear() =>
            ShouldFollowSuit = false;
    }
}