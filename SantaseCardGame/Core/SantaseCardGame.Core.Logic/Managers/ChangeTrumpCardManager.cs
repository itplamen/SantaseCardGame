namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class ChangeTrumpCardManager : IPlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCardManager(IDeckState deckState, ITrickState trickState, IPlayerActionValidator playerActionValidator)
        {
            this.deckState = deckState;
            this.trickState = trickState;
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
                    trickState.Notify("Change");

                    return;
                }
            }

            trickState.Notify("Can't");
        }
    }
}