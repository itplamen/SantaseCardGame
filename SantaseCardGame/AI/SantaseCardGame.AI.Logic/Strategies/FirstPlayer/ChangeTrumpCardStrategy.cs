namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class ChangeTrumpCardStrategy : BasePlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCardStrategy(ITrickState trickState, IDeckState deckState, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.deckState = deckState;
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction SelectStrategy(Player player)
        {
            if (playerActionValidator.CanChangeTrump(player))
            {
                Card nineOfTrumps = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == deckState.TrumpCard.Suit);

                if (nineOfTrumps != null)
                {
                    return new PlayerAction(PlayerActionType.ChangeTrumpCard, deckState.TrumpCard);
                }
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}
