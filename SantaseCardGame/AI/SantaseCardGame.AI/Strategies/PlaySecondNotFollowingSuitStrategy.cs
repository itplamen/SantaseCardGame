namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlaySecondNotFollowingSuitStrategy : IChoosePlayerActionStrategy
    {
        private readonly ITrickState trickState;
        private readonly IEnumerable<IPlayCard> playCardDecisions;

        public PlaySecondNotFollowingSuitStrategy(ITrickState trickState, IEnumerable<IPlayCard> playCardDecisions)
        {
            this.trickState = trickState;
            this.playCardDecisions = playCardDecisions;
        }

        public PlayerAction ChoosePlayerAction(Player player)
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