namespace SantaseCardGame.Core.Logic.Announcements
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class AnnouncementChecker : IAnnouncementChecker
    {
        private readonly IGameState gameState;
        private readonly IGameStorage gameStorage;
        private readonly IPlayerActionValidator playerActionValidator;

        public AnnouncementChecker(IGameState gameState, IGameStorage gameStorage, IPlayerActionValidator playerActionValidator)
        {
            this.gameState = gameState;
            this.gameStorage = gameStorage;
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
                    Game game = gameStorage.Get(gameState.CurrentGameId);

                    if (card.Suit == game.Deck.TrumpCard.Suit)
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
                .Where(x => x.Count() == gameState.MarriageCardsCount)
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
