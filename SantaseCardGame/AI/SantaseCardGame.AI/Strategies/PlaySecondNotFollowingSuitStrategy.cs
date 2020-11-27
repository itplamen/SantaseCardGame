namespace SantaseCardGame.AI.Strategies
{
    using System.Collections.Generic;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlaySecondNotFollowingSuitStrategy : BasePlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlaySecondNotFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
            : base(playLogics)
        {
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public override bool ShouldPlay(Player player)
        {
            return player.Position == PlayerPosition.Second &&
                player.Position == trickState.PlayerTurn &&
                !deckState.ShouldFollowSuit;
        }
    }
}