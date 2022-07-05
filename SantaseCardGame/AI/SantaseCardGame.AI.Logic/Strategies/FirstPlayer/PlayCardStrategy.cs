namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayCardStrategy : BasePlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly IAnnouncementChecker announcementChecker;

        public PlayCardStrategy(IDeckState deckState, IAnnouncementChecker announcementChecker)
        {
            this.deckState = deckState;
            this.announcementChecker = announcementChecker;
        }

        protected override PlayerAction SelectStrategy(Player player)
        {
            IEnumerable<Card> marriages = announcementChecker.GetMarriages(player.Cards);
            IEnumerable<Card> playCards = player.Cards
                .Where(x => !marriages.Contains(x))
                .OrderBy(x => x.Type);

            if (playCards.Any())
            {
                Card playCard = playCards.FirstOrDefault(x => x.Suit != deckState.TrumpCard.Suit);

                if (playCard != null)
                {
                    return new PlayerAction(PlayerActionType.PlayCard, playCard);
                }
            }

            return new PlayerAction(PlayerActionType.PlayCard, playCards.First());
        }
    }
}
