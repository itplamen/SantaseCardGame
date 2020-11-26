namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlaySecondFollowingSuitStrategy : IPlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly IEnumerable<IPlayCard> playCardDecisions;

        public PlaySecondFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayCard> playCardDecisions)
        {
            this.deckState = deckState;
            this.trickState = trickState;
            this.playCardDecisions = playCardDecisions;
        }

        public bool ShouldPlay(Player player)
        {
            return player.Position == PlayerPosition.Second &&
                player.Position == trickState.PlayerTurn &&
                deckState.ShouldFollowSuit;
        }

        public PlayerAction Play(Player player)
        {
            Card opponentCard = trickState.Cards.First(x => x.Key != player.Position).Value;

            foreach (var cardDecision in playCardDecisions)
            {
                Card playCard = cardDecision.PlayCard(player, opponentCard);

                if (playCard != null)
                {
                    return new PlayerAction(PlayerActionType.PlayCard, playCard);
                }
            }

            return new PlayerAction(PlayerActionType.PlayCard);
        }
    }
}