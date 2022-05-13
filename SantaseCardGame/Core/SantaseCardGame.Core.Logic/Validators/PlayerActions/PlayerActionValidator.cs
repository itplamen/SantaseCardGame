namespace SantaseCardGame.Core.Logic.Validators.PlayerActions
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayerActionValidator : IPlayerActionValidator
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayerActionValidator(IGameState gameState, IDeckState deckState, ITrickState trickState)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public bool CanAnnounce(Player player)
        {
            return player.Position == trickState.PlayerTurn &&
                !trickState.Cards.Any() &&
                player.Hands.Any();
        }

        public bool CanChangeTrump(Player player)
        {
            return CanPerformAction(player);
        }

        public bool CanCloseDeck(Player player)
        {
            return CanPerformAction(player);
        }

        private bool CanPerformAction(Player player)
        {
            return deckState.CardsLeft >= gameState.DeckMinCardsBeforeClosing &&
                deckState.ClosedBy == PlayerPosition.None &&
                !deckState.ShouldFollowSuit &&
                player.Position == trickState.PlayerTurn &&
                !trickState.Cards.Any() &&
                player.Hands.Any();
        }
    }
}
