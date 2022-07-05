namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class AnnounceMarriageStrategy : BasePlayerActionStrategy
    {
        private readonly IGameState gameState;
        private readonly IGameStorage gameStorage;
        private readonly IAnnouncementChecker announcementChecker;
        private readonly IPlayerActionValidator playerActionValidator;

        public AnnounceMarriageStrategy(IGameState gameState, IGameStorage gameStorage, IAnnouncementChecker announcementChecker, IPlayerActionValidator playerActionValidator)
        {
            this.gameState = gameState;
            this.gameStorage = gameStorage;
            this.announcementChecker = announcementChecker;
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction SelectStrategy(Player player)
        {
            if (playerActionValidator.CanAnnounce(player))
            {
                IEnumerable<Card> marriages = announcementChecker.GetMarriages(player.Cards);

                if (marriages.Any())
                {
                    Card queen = marriages.First(x => x.Type == CardType.Queen);
                    Game game = gameStorage.Get(gameState.CurrentGameId);

                    if (queen.Suit == game.Deck.TrumpCard.Suit)
                    {
                        return new PlayerAction(PlayerActionType.AnnounceCardMarriage, queen, Announce.Forty);
                    }

                    return new PlayerAction(PlayerActionType.AnnounceCardMarriage, queen, Announce.Twenty);
                }
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}
