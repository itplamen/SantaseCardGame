namespace SantaseCardGame.Core.Logic.Managers
{
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class CloseDeckManager : IPlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeckManager(IDeckState deckState, IPlayerActionValidator playerActionValidator)
        {
            this.deckState = deckState;
            this.playerActionValidator = playerActionValidator;
        }

        public bool ShouldManage(PlayerAction playerAction)
        {
            return playerAction.Type == PlayerActionType.CloseDeck;
        }

        public void Manage(PlayerAction playerAction, Player player)
        {
            if (playerActionValidator.CanCloseDeck(player))
            {
                deckState.IsClosed = true;

                // TODO: Notify
            }

            // TODO: Notify when cant
        }
    }
}