namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class ChangeTrumpCard : IPlayLogic
    {
        private readonly ITrickState trickState;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCard(ITrickState trickState, IPlayerActionValidator playerActionValidator)
        {
            this.trickState = trickState;
            this.playerActionValidator = playerActionValidator;
        }

        public PlayerAction Play(Player player)
        {
            if (playerActionValidator.CanChangeTrump(player))
            {
                Card nineOfTrumps = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == trickState.TrumpCardSuit);

                if (nineOfTrumps != null)
                {
                    return new PlayerAction(PlayerActionType.ChangeTrump, nineOfTrumps);
                }
            }

            return new PlayerAction(PlayerActionType.ChangeTrump);
        }
    }
}