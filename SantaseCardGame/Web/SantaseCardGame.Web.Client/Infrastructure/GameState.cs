namespace SantaseCardGame.Web.Client.Infrastructure
{
    using System;

    using SantaseCardGame.Data.Models;

    public class GameState
    {
        public bool IsClosed { get; set; }

        public event Action OnCloseDeck;

        public event Action<Card> OnChangeTrumpCard;

        public event Action<Card> OnExchangeTrumpCardForNineOfTrumps;

        public void CloseDeck()
        {
            if (!IsClosed)
            {
                OnCloseDeck?.Invoke();
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