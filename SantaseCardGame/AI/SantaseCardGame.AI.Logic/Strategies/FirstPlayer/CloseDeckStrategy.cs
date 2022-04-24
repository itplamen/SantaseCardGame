namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class CloseDeckStrategy : BasePlayerActionStrategy
    {
        private const int ENOUGH_POINTS_REQUIRED_FOR_CLOSING = 50;
        private const int REMAINING_POINTS_REQUIRED_FOR_CLOSING = 20;

        private readonly IGameRules gameRules;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeckStrategy(ITrickState trickState, IGameRules gameRules, IAnnouncementChecker announcementChecker, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.gameRules = gameRules;
            this.announcementChecker = announcementChecker;
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction SelectStrategy(Player player)
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
            return player.Points >= gameRules.RoundHalfPoints && 
                player.Cards.Sum(x => (int)x.Type) >= REMAINING_POINTS_REQUIRED_FOR_CLOSING;
        }

        private bool HasEnoughPointsWithExistingAnnouncements(Player player)
        {
            return player.Points >= gameRules.RoundHalfPoints && 
                announcementChecker.GetMarriages(player.Cards).Any();
        }
    }
}
