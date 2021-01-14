namespace SantaseCardGame.AI.Play.First
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class CloseDeck : BasePlayLogic
    {
        private readonly IGameState gameState;
        private readonly IAnnounceProvider announceProvider;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeck(ITrickState trickState, IGameState gameState, IAnnounceProvider announceProvider, IPlayerActionValidator playerActionValidator)
            : base(trickState)
        {
            this.gameState = gameState;
            this.announceProvider = announceProvider;
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
                    (player.Points >= gameState.RoundHalfPoints && player.Cards.Sum(x => (int)x.Type) >= 20) ||
                    (player.Points >= gameState.RoundHalfPoints && announceProvider.GetMarriages(player).Any());
        }
    }
}