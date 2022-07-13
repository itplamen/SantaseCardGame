namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System.Linq;

    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class CloseDeckStrategy : IPlayerActionStrategy
    {
        private const int ENOUGH_POINTS_REQUIRED_FOR_CLOSING = 50;
        private const int REMAINING_POINTS_REQUIRED_FOR_CLOSING = 20;

        private readonly IGameState gameState;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeckStrategy(IGameState gameState, IAnnouncementChecker announcementChecker, IPlayerActionValidator playerActionValidator)
        {
            this.gameState = gameState;
            this.announcementChecker = announcementChecker;
            this.playerActionValidator = playerActionValidator;
        }

        public PlayerAction ChooseAction(Player player)
        {
            if (playerActionValidator.CanCloseDeck(player) && ShouldClose(player))
            {
                return new PlayerAction(PlayerActionType.CloseDeck);
            }

            return new PlayerAction(PlayerActionType.None);
        }

        private bool ShouldClose(Player player)
        {
            return player.Points >= ENOUGH_POINTS_REQUIRED_FOR_CLOSING ||
                HasEnoughPointsWithExistingCards(player) ||
                HasEnoughPointsWithExistingAnnouncements(player);
        }

        private bool HasEnoughPointsWithExistingCards(Player player)
        {
            return player.Points >= gameState.RoundHalfPoints && 
                player.Cards.Sum(x => (int)x.Type) >= REMAINING_POINTS_REQUIRED_FOR_CLOSING;
        }

        private bool HasEnoughPointsWithExistingAnnouncements(Player player)
        {
            return player.Points >= gameState.RoundHalfPoints && 
                announcementChecker.GetMarriages(player.Cards).Any();
        }
    }
}
