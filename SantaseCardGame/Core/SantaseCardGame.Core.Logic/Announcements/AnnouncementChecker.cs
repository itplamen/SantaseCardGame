namespace SantaseCardGame.Core.Logic.Announcements
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class AnnouncementChecker : IAnnouncementChecker
    {
        private readonly IGameRules gameRules;
        private readonly IDeckState deckState;
        private readonly IPlayerActionValidator playerActionValidator;

        public AnnouncementChecker(IGameRules gameRules, IDeckState deckState, IPlayerActionValidator playerActionValidator)
        {
            this.gameRules = gameRules;
            this.deckState = deckState;
            this.playerActionValidator = playerActionValidator;
        }

        public Announce GetAnnouncement(Player player, Card card)
        {
            if (playerActionValidator.CanAnnounce(player))
            {
                bool hasMarriage = GetMarriages(player.Cards)
                    .Any(x => x.Name == card.Name);

                if (hasMarriage)
                {
                    if (card.Suit == deckState.TrumpCard.Suit)
                    {
                        return Announce.Forty;
                    }

                    return Announce.Twenty;
                }
            }

            return Announce.None;
        }

        public IEnumerable<Card> GetMarriages(IEnumerable<Card> cards)
        {
            return cards.Where(x => x.Type == CardType.Queen || x.Type == CardType.King)
                .GroupBy(x => x.Suit)
                .Where(x => x.Count() == gameRules.MarriageCardsCount)
                .SelectMany(x => x);
        }

        public CardType MarriageCardTypeToSearch(Card card)
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
