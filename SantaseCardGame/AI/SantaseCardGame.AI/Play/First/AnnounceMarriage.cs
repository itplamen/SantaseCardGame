namespace SantaseCardGame.AI.Play.First
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class AnnounceMarriage : BasePlayLogic
    {
        private readonly IDeckState deckState;
        private readonly IAnnounceProvider announceProvider;
        private readonly IPlayerActionValidator playerActionValidator;

        public AnnounceMarriage(ITrickState trickState, IDeckState deckState, IAnnounceProvider announceProvider, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.deckState = deckState;
            this.announceProvider = announceProvider;
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            if (playerActionValidator.CanAnnounce(player))
            {
                IEnumerable<Card> marriages = announceProvider.GetMarriages(player);

                if (marriages.Any())
                {
                    Card queen = marriages.First(x => x.Type == CardType.Queen);

                    if (queen.Suit == deckState.TrumpCard.Suit)
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