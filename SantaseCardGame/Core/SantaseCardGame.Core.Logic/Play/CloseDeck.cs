namespace SantaseCardGame.Core.Logic.Play
{
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class CloseDeck : BaseActionPlaying
    {
        private readonly IDeckState deckState;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeck(ITrickState trickState, IDeckState deckState, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.deckState = deckState;
            this.playerActionValidator = playerActionValidator;
        }

        public override PlayerActionResult Play(PlayerAction playerAction, Player player)
        {
            if (ShouldPlay(playerAction, player))
            {
                deckState.ShouldFollowSuit = true;
                deckState.ClosedBy = player.Position;

                return new PlayerActionResult(true, "Deck closed");
            }

            return new PlayerActionResult(false);
        }

        protected override bool ShouldPlay(PlayerAction playerAction, Player player)
        {
            return base.ShouldPlay(playerAction, player) &&
                playerActionValidator.CanCloseDeck(player) &&
                playerAction.Type == PlayerActionType.CloseDeck;
        }
    }
}
