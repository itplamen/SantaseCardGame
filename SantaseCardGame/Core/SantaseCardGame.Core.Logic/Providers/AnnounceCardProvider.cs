namespace SantaseCardGame.Core.Logic.Providers
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class AnnounceCardProvider : IAnnounceCardProvider
    {
        private readonly IDeckState deckState;
        private readonly IPlayerActionValidator playerActionValidator;

        public AnnounceCardProvider(IDeckState deckState, IPlayerActionValidator playerActionValidator)
        {
            this.deckState = deckState;
            this.playerActionValidator = playerActionValidator;
        }

        public PlayerAction GetAnnounce(Player player, Card card)
        {
            if (playerActionValidator.CanAnnounce(player))
            {
                CardType cardType = GetMarriageCardType(card);
                bool hasMarriage = player.Cards.FirstOrDefault(x => x.Suit == card.Suit && x.Type == cardType && x.Type != CardType.None) != null;

                if (hasMarriage)
                {
                    if (card.Suit == deckState.TrumpCard.Suit)
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
    }
}