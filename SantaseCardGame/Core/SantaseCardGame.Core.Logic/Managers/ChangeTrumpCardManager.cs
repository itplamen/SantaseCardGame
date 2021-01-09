namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class ChangeTrumpCardManager : BasePlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCardManager(IDeckState deckState, IGameState gameState, ITrickState trickState, IPlayerActionValidator playerActionValidator)
            : base(gameState, trickState)
        {
            this.deckState = deckState;
            this.gameState = gameState;
            this.playerActionValidator = playerActionValidator;
        }

        public override bool ShouldManage(PlayerAction playerAction, Player player)
        {
            return base.ShouldManage(playerAction, player) &&
                playerAction.Type == PlayerActionType.ChangeTrump && 
                playerAction.Card != null;
        }

        public override void Manage(PlayerAction playerAction, Player player)
        {
            if (playerActionValidator.CanChangeTrump(player))
            {
                Card nineOfTrumpsCard = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == playerAction.Card.Suit);

                if (nineOfTrumpsCard != null)
                {
                    int nineOfTrumpsIndex = player.Cards.FindIndex(x => x.Name == nineOfTrumpsCard.Name);
                    player.Cards[nineOfTrumpsIndex] = playerAction.Card;
                    
                    deckState.ExchangeTrumpCardForNineOfTrumps(nineOfTrumpsCard);
                    gameState.ShowMessage(player.Position, $"Trump card changed");
                }
            }
        }
    }
}