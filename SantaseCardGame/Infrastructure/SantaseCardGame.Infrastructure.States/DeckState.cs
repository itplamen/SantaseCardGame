namespace SantaseCardGame.Infrastructure.States
{
    using System;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class DeckState : IDeckState
    {
        public bool ShouldFollowSuit { get; set; }

        public Card TrumpCard { get; set; }

        public PlayerPosition ClosedBy { get; set; }

        public int CardsLeft { get; set; }

        public event Action<PlayerAction> OnChangeTrumpCard;

        public event Action<Card> OnExchangeTrumpCardForNineOfTrumps;

        public void ChangeTrumpCard(PlayerAction playerAction)
        {
            if (ClosedBy == PlayerPosition.None)
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