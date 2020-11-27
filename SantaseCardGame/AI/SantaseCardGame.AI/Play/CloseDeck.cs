namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class CloseDeck : BasePlayLogic
    {
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeck(ITrickState trickState, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.playerActionValidator = playerActionValidator;
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            if (playerActionValidator.CanCloseDeck(player) && ShouldClose(player))
            {
                return new PlayerAction(PlayerActionType.CloseDeck);
            }

            return new PlayerAction(PlayerActionType.None);
        }

        private bool ShouldClose(Player player)
        {
            return player.Points >= 50 ||
                    (player.Points >= 33 && player.Cards.Sum(x => (int)x.Type) >= 20) ||
                    (player.Points >= 33 && GetMarriages(player).Any());
        }
    }
}