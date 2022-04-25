namespace SantaseCardGame.Core.Logic.Play
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayCard : BaseActionPlaying
    {
        private readonly ITrickState trickState;
        private readonly IEnumerable<ICardPlayableValidator> playCardValidators;

        public PlayCard(ITrickState trickState, IEnumerable<ICardPlayableValidator> playCardValidators)
            : base(trickState)
        {
            this.trickState = trickState;
            this.playCardValidators = playCardValidators;
        }

        public override PlayerActionResult Play(PlayerAction playerAction, Player player)
        {
            if (ShouldPlay(playerAction, player))
            {
                Card opponentCard = trickState.Cards.FirstOrDefault(x => x.Key != player.Position).Value;

                if (opponentCard != null)
                {
                    var canPlay = playCardValidators.All(x => x.CanPlay(player, playerAction.Card, opponentCard));

                    if (!canPlay)
                    {
                        return new PlayerActionResult(false);
                    }
                }

                player.RemoveCard(playerAction.Card);
                trickState.AddCard(playerAction.Card, player.Position);

                return new PlayerActionResult(true);
            }

            return new PlayerActionResult(false);
        }

        protected override bool ShouldPlay(PlayerAction playerAction, Player player)
        {
            return base.ShouldPlay(playerAction, player) &&
                playerAction.Type == PlayerActionType.PlayCard &&
                playerAction.Card != null;
        }
    }
}
