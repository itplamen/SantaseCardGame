namespace SantaseCardGame.Core.Logic.Managers
{
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class CloseDeckManager : BasePlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeckManager(IDeckState deckState, IGameState gameState, ITrickState trickState, IPlayerActionValidator playerActionValidator)
            : base(gameState, trickState)
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
                deckState.ShouldFollowSuit = true;
                deckState.ClosedBy = player.Position;
                gameState.ShowMessage(player.Position, "Closed");
            }
        }
    }
}