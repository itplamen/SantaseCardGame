namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class AnnounceMarriageStrategy : BasePlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IPlayerActionValidator playerActionValidator;

        public AnnounceMarriageStrategy(ITrickState trickState, IDeckState deckState, IAnnouncementChecker announcementChecker, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.deckState = deckState;
            this.announcementChecker = announcementChecker;
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction PlayStrategy(Player player)
        {
            if (playerActionValidator.CanAnnounce(player))
            {
                IEnumerable<Card> marriages = announcementChecker.GetMarriages(player.Cards);

                if (marriages.Any())
                {
                    Card queen = marriages.First(x => x.Type == CardType.Queen);

                    if (queen.Suit == deckState.TrumpCard.Suit)
                    {
                        return new PlayerAction(PlayerActionType.Announce, queen, Announce.Forty);
                    }

                    return new PlayerAction(PlayerActionType.Announce, queen, Announce.Twenty);
                }
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}
