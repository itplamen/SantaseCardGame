namespace SantaseCardGame.AI.Play
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class AnnounceMarriage : BasePlayLogic
    {
        private readonly ITrickState trickState;
        private readonly IPlayerActionValidator playerActionValidator;

        public AnnounceMarriage(ITrickState trickState, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.trickState = trickState;
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            if (playerActionValidator.CanAnnounce(player))
            {
                IEnumerable<Card> marriages = GetMarriages(player);

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

            return new PlayerAction(PlayerActionType.None);
        }
    }
}