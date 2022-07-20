namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System.Linq;

    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class CloseDeckStrategy : IPlayerActionStrategy
    {
        private const int MIN_TRUMP_CARDS_REQUIRED_TO_CLOSE = 4;

        private readonly IGameState gameState;
        private readonly IStorage<Game> gameStorage;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeckStrategy(IGameState gameState, IStorage<Game> gameStorage, IAnnouncementChecker announcementChecker, IPlayerActionValidator playerActionValidator)
        {
            this.gameState = gameState;
            this.gameStorage = gameStorage;
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
            return HasEnoughPointsWithExistingCards(player) ||
                HasEnoughPointsWithExistingTrumpCards(player) ||
                HasEnoughPointsWithExistingAnnouncements(player);
        }

        private bool HasEnoughPointsWithExistingCards(Player player)
        {
            return player.Points >= gameState.RoundHalfPoints && 
                player.Cards.Sum(x => (int)x.Type) >= gameState.RoundHalfPoints;
        }

        private bool HasEnoughPointsWithExistingTrumpCards(Player player)
        {
            var game = gameStorage.Get(gameState.CurrentGameId);

            return player.Points >= gameState.RoundHalfPoints &&
                player.Cards.Count(x => x.Suit == game.Deck.TrumpCard.Suit) >= MIN_TRUMP_CARDS_REQUIRED_TO_CLOSE;
        }

        private bool HasEnoughPointsWithExistingAnnouncements(Player player)
        {
            return player.Points >= gameState.RoundHalfPoints && 
                announcementChecker.GetMarriages(player.Cards).Any();
        }
    }
}
