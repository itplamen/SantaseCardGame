namespace SantaseCardGame.AI.Play
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class AnnounceMarriage : IPlayLogic
    {
        private const int MARRIAGE_CARDS_COUNT = 2;

        private readonly ITrickState trickState;
        private readonly IPlayerActionValidator playerActionValidator;

        public AnnounceMarriage(ITrickState trickState, IPlayerActionValidator playerActionValidator)
        {
            this.trickState = trickState;
            this.playerActionValidator = playerActionValidator;
        }

        public PlayerAction Play(Player player)
        {
            if (playerActionValidator.CanAnnounce(player))
            {
                IEnumerable<Card> marriages = player.Cards
                    .Where(x => x.Type == CardType.Queen || x.Type == CardType.King)
                    .GroupBy(x => x.Suit)
                    .Where(x => x.Count() == MARRIAGE_CARDS_COUNT)
                    .SelectMany(x => x);

                if (marriages.Any())
                {
                    Card queen = marriages.First(x => x.Type == CardType.Queen);

                    if (queen.Suit == trickState.TrumpCardSuit)
                    {
                        return new PlayerAction(PlayerActionType.Announce, queen, Announce.Forty);
                    }

                    return new PlayerAction(PlayerActionType.Announce, queen, Announce.Twenty);
                }
            }

            return new PlayerAction(PlayerActionType.Announce);
        }
    }
}