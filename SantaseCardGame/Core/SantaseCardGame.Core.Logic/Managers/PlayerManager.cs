namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayerManager : IPlayerManager
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayerManager(IDeckState deckState, ITrickState trickState)
        {
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public bool ChangeTrumpCard(Player player, Card trumpCard)
        {
            if (CanPerformAction(player))
            {
                Card nineOfTrumpsCard = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == trumpCard.Suit);

                if (nineOfTrumpsCard != null)
                {
                    int nineOfTrumpsIndex = player.Cards.FindIndex(x => x.Name == nineOfTrumpsCard.Name);
                    player.Cards[nineOfTrumpsIndex] = trumpCard;
                    deckState.ExchangeTrumpCardForNineOfTrumps(nineOfTrumpsCard);

                    return true;
                }
            }

            return false;
        }

        public bool CloseDeck(Player player)
        {
            if (CanPerformAction(player))
            {
                deckState.IsClosed = true;
            }

            return deckState.IsClosed;
        }

        public Announce PlayCard(Player player, Card card)
        {
            Card opponentTrickCard = trickState.Cards.FirstOrDefault(x => x.Key != player.Position).Value;

            if (player.Position == trickState.PlayerTurn)
            {
                if (opponentTrickCard != null && deckState.ShouldFollowSuit)
                {
                    bool hasSameSuit = player.Cards.Any(x => x.Suit == opponentTrickCard.Suit);

                    if (hasSameSuit && card.Suit != opponentTrickCard.Suit)
                    {
                        return Announce.None;
                    }
                }

                Announce announce = GetAnnounce(player, card);

                player.Cards.Remove(card);
                trickState.AddCard(card, player.Position);

                return announce;
            }

            return Announce.None;
        }

        private Announce GetAnnounce(Player player, Card card)
        {
            if (CanPerformAction(player))
            {
                Card marriageCard = GetMarriageCard(player, card);

                if (marriageCard != null)
                {
                    if (marriageCard.Suit == trickState.TrumpCardSuit)
                    {
                        return Announce.Forty;
                    }

                    return Announce.Twenty;
                }
            }

            return Announce.None;
        }

        private Card GetMarriageCard(Player player, Card card)
        {
            Card marriageCard = null;
            CardType cardType = GetMarriageCardType(card);

            if (cardType != CardType.None)
            {
                marriageCard = player.Cards.FirstOrDefault(x => x.Suit == card.Suit && x.Type == cardType);
            }

            return marriageCard;
        }
        
        private CardType GetMarriageCardType(Card card)
        {
            switch (card.Type)
            {
                case CardType.Queen:
                    return CardType.King;
                case CardType.King:
                    return CardType.Queen;
                default:
                    return CardType.None;
            }
        }

        private bool CanPerformAction(Player player)
        {
            return player.Position == trickState.PlayerTurn &&
                !deckState.ShouldFollowSuit &&
                !trickState.Cards.Any() &&
                !deckState.IsClosed &&
                player.Hands.Any();
        }
    }
}
