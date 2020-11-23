namespace SantaseCardGame.Core.Logic.Providers
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class AnnounceCardProvider : IAnnounceCardProvider
    {
        private readonly ITrickState trickState;

        public AnnounceCardProvider(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        public PlayerAction GetAnnounce(Player player, Card card)
        {
            if (CanAnnounce(player, card))
            {
                CardType cardType = GetMarriageCardType(card);
                bool hasMarriage = player.Cards.FirstOrDefault(x => x.Suit == card.Suit && x.Type == cardType && x.Type != CardType.None) != null;

                if (hasMarriage)
                {
                    if (card.Suit == trickState.TrumpCardSuit)
                    {
                        return new PlayerAction(PlayerActionType.Announce, card, Announce.Forty);
                    }

                    return new PlayerAction(PlayerActionType.Announce, card, Announce.Twenty);
                }
            }

            return new PlayerAction(PlayerActionType.Announce, Announce.None);
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

        private bool CanAnnounce(Player player, Card card)
        {
            return card != null &&
                player.Position == trickState.PlayerTurn &&
                !trickState.Cards.Any() &&
                player.Hands.Any();
        }
    }
}