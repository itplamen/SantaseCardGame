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

        public PlayCard(IGameState gameState, ITrickState trickState, IEnumerable<ICardPlayableValidator> playCardValidators)
            : base(gameState, trickState)
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
                    foreach (var validator in playCardValidators)
                    {
                        bool canPlay = validator.CanPlay(player, playerAction.Card, opponentCard);

                        if (!canPlay)
                        {
                            return new PlayerActionResult(false, validator.Message);
                        }
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
