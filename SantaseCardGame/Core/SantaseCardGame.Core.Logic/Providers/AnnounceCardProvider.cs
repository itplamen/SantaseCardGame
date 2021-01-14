namespace SantaseCardGame.Core.Logic.Providers
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class AnnounceCardProvider : IAnnounceCardProvider
    {
        private const int MARRIAGE_CARDS_COUNT = 2;

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
                bool hasMarriage = GetMarriages(player)
                    .Any(x => x.Name == card.Name);

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

        public IEnumerable<Card> GetMarriages(Player player)
        {
            return player.Cards
                .Where(x => x.Type == CardType.Queen || x.Type == CardType.King)
                .GroupBy(x => x.Suit)
                .Where(x => x.Count() == MARRIAGE_CARDS_COUNT)
                .SelectMany(x => x);
        }

        public CardType AnnounceCardTypeToSearch(Card card)
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