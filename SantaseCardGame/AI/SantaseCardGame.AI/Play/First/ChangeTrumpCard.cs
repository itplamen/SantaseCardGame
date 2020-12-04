namespace SantaseCardGame.AI.Play.First
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class ChangeTrumpCard : BasePlayLogic
    {
        private readonly IDeckState deckState;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCard(ITrickState trickState, IDeckState deckState, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.deckState = deckState;
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            if (playerActionValidator.CanChangeTrump(player))
            {
                Card nineOfTrumps = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == deckState.TrumpCard.Suit);

                if (nineOfTrumps != null)
                {
                    return new PlayerAction(PlayerActionType.ChangeTrump, deckState.TrumpCard);
                }
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}