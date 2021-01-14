namespace SantaseCardGame.AI.Play.First
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class PlayCard : BasePlayLogic
    {
        private readonly IDeckState deckState;
        private readonly IAnnounceProvider announceProvider;

        public PlayCard(ITrickState trickState, IDeckState deckState, IAnnounceProvider announceProvider)
            : base(trickState)
        {
            this.deckState = deckState;
            this.announceProvider = announceProvider;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            IEnumerable<Card> marriages = announceProvider.GetMarriages(player);
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