namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class ChangeTrumpCard : BasePlayLogic
    {
        private readonly ITrickState trickState;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCard(ITrickState trickState, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.trickState = trickState;
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            if (playerActionValidator.CanChangeTrump(player))
            {
                Card nineOfTrumps = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == trickState.TrumpCardSuit);

                return new PlayerAction(PlayerActionType.ChangeTrump, nineOfTrumps);
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}