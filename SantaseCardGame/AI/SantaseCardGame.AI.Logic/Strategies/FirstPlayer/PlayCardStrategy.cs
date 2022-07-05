namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayCardStrategy : BasePlayerActionStrategy
    {
        private readonly IGameState gameState;
        private readonly IGameStorage gameStorage;
        private readonly IAnnouncementChecker announcementChecker;

        public PlayCardStrategy(IGameState gameState, IGameStorage gameStorage, IAnnouncementChecker announcementChecker)
        {
            this.gameState = gameState;
            this.gameStorage = gameStorage;
            this.announcementChecker = announcementChecker;
        }

        protected override PlayerAction SelectStrategy(Player player)
        {
            IEnumerable<Card> marriages = announcementChecker.GetMarriages(player.Cards);
            IEnumerable<Card> playCards = player.Cards
                .Where(x => !marriages.Contains(x))
                .OrderBy(x => x.Type);

            if (playCards.Any())
            {
                Game game = gameStorage.Get(gameState.CurrentGameId);
                Card playCard = playCards.FirstOrDefault(x => x.Suit != game.Deck.TrumpCard.Suit);

                if (playCard != null)
                {
                    return new PlayerAction(PlayerActionType.PlayCard, playCard);
                }
            }

            return new PlayerAction(PlayerActionType.PlayCard, playCards.First());
        }
    }
}
