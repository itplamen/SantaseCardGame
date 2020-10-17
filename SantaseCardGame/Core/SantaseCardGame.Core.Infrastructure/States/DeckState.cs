namespace SantaseCardGame.Core.Infrastructure.States
{
    using System;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class DeckState : IDeckState
    {
        public event Action OnClose;

        public event Action<Card> OnChangeTrumpCard;

        public event Action<Card> OnExchangeTrumpCardForNineOfTrumps;

        public bool IsClosed { get; set; }

        public int CardsLeft { get; set; }

        public bool ShouldFollowSuit { get; set; }

        public void Close()
        {
            if (!IsClosed)
            {
                OnClose?.Invoke();

                ShouldFollowSuit = true;
            }
        }

        public void ChangeTrumpCard(Card trumpCard)
        {
            if (!IsClosed)
            {
                OnChangeTrumpCard?.Invoke(trumpCard);
            }
        }

        public void ExchangeTrumpCardForNineOfTrumps(Card nineOfTrumpsCard)
        {
            OnExchangeTrumpCardForNineOfTrumps?.Invoke(nineOfTrumpsCard);
        }
    }
}