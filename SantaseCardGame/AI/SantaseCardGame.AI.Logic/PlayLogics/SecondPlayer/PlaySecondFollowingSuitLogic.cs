namespace SantaseCardGame.AI.Logic.PlayLogics.SecondPlayer
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlaySecondFollowingSuitLogic : BasePlayLogic
    {
        private readonly ITrickState trickState;
        private readonly IDeckState deckState;

        public PlaySecondFollowingSuitLogic(ITrickState trickState, IDeckState deckState, IEnumerable<IPlayerActionStrategy> strategies)
            : base(trickState, strategies)
        {
            this.trickState = trickState;
            this.deckState = deckState;
        }

        public override bool ShouldPlay(Player player)
        {
            return base.ShouldPlay(player) &&
                deckState.ShouldFollowSuit &&
                trickState.Cards.Any();
        }
    }
}
