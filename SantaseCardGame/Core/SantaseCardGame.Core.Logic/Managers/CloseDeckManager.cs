namespace SantaseCardGame.Core.Logic.Managers
{
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class CloseDeckManager : BasePlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeckManager(IDeckState deckState, IGameState gameState, ITrickState trickState, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.deckState = deckState;
            this.gameState = gameState;
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
                deckState.ClosedBy = player.Position;
                gameState.ShowMessage("Close");

                return;
            }

            gameState.ShowMessage("Cant close");
        }
    }
}