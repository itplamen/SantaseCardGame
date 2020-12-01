namespace SantaseCardGame.AI.Play.First
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class CloseDeck : BasePlayLogic
    {
        private readonly IGameRules gameRules;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeck(ITrickState trickState, IGameRules gameRules, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.gameRules = gameRules;
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
                    (player.Points >= gameRules.RoundHalfPoints && player.Cards.Sum(x => (int)x.Type) >= 20) ||
                    (player.Points >= gameRules.RoundHalfPoints && GetMarriages(player).Any());
        }
    }
}