namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class ChangeTrumpCardManager : IPlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCardManager(IDeckState deckState, IPlayerActionValidator playerActionValidator)
        {
            this.deckState = deckState;
            this.playerActionValidator = playerActionValidator;
        }

        public bool ShouldManage(PlayerAction playerAction)
        {
            return playerAction.Type == PlayerActionType.ChangeTrump && playerAction.Card != null;
        }

        public void Manage(PlayerAction playerAction, Player player)
        {
            if (playerActionValidator.CanChangeTrump(player))
            {
                Card nineOfTrumpsCard = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == playerAction.Card.Suit);

                if (nineOfTrumpsCard != null)
                {
                    int nineOfTrumpsIndex = player.Cards.FindIndex(x => x.Name == nineOfTrumpsCard.Name);
                    player.Cards[nineOfTrumpsIndex] = playerAction.Card;
                    deckState.ExchangeTrumpCardForNineOfTrumps(nineOfTrumpsCard);

                    // TODO: Notify
                }
            }

            // TODO: Notify when cant
        }
    }
}