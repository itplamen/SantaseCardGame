namespace SantaseCardGame.Core.Infrastructure.States
{
    using System;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class DeckState : IDeckState
    {
        public event Action<PlayerAction> OnClose;

        public event Action<PlayerAction> OnChangeTrumpCard;

        public event Action<Card> OnExchangeTrumpCardForNineOfTrumps;

        public PlayerPosition ClosedBy { get; set; }

        public int CardsLeft { get; set; }

        public bool ShouldFollowSuit { get; set; }

        public void Close()
        {
            if (ClosedBy == PlayerPosition.NoOne)
            {
                OnClose?.Invoke(new PlayerAction(PlayerActionType.CloseDeck));

                ShouldFollowSuit = true;
            }
        }

        public void ChangeTrumpCard(PlayerAction playerAction)
        {
            if (ClosedBy == PlayerPosition.NoOne)
            {
                OnChangeTrumpCard?.Invoke(playerAction);
            }
        }

        public void ExchangeTrumpCardForNineOfTrumps(Card nineOfTrumpsCard)
        {
            OnExchangeTrumpCardForNineOfTrumps?.Invoke(nineOfTrumpsCard);
        }
    }
}