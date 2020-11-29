namespace SantaseCardGame.AI.Play.First
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayCard : BasePlayLogic
    {
        private readonly ITrickState trickState;

        public PlayCard(ITrickState trickState)
            : base(trickState)
        {
            this.trickState = trickState;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            IEnumerable<Card> marriages = GetMarriages(player);
            IEnumerable<Card> playCards = player.Cards
                .Where(x => !marriages.Contains(x))
                .OrderBy(x => x.Type);

            if (playCards.Any())
            {
                Card playCard = playCards.FirstOrDefault(x => x.Suit != trickState.TrumpCardSuit);

                if (playCard != null)
                {
                    return new PlayerAction(PlayerActionType.PlayCard, playCard);
                }
            }

            return new PlayerAction(PlayerActionType.PlayCard, playCards.First());
        }
    }
}