namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class PlaySecondFollowingSuitStrategy : BasePlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlaySecondFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
            : base(trickState, playLogics)
        {
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public override bool ShouldPlay(Player player)
        {
            return base.ShouldPlay(player) &&
                deckState.ShouldFollowSuit &&
                trickState.Cards.Any();
        }
    }
}