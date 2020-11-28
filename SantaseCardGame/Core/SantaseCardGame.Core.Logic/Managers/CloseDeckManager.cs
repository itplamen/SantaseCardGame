namespace SantaseCardGame.Core.Logic.Managers
{
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class CloseDeckManager : BasePlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeckManager(IDeckState deckState, ITrickState trickState, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.deckState = deckState;
            this.trickState = trickState;
            this.playerActionValidator = playerActionValidator;
        }

        public override bool ShouldManage(PlayerAction playerAction, Player player)
        {
            return base.ShouldManage(playerAction, player) &&
                playerAction.Type == PlayerActionType.CloseDeck;
        }

        public override void Manage(PlayerAction playerAction, Player player)
        {
            if (playerActionValidator.CanCloseDeck(player))
            {
                deckState.IsClosed = true;
                trickState.Notify("Close");

                return;
            }

            trickState.Notify("Cant close");
        }
    }
}