namespace SantaseCardGame.Core.Logic.Validators
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayerActionValidator : IPlayerActionValidator
    {
        private const int DECK_CARDS_REQUIRED = 4;

        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayerActionValidator(IDeckState deckState, ITrickState trickState)
        {
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
            return deckState.CardsLeft >= DECK_CARDS_REQUIRED &&
                deckState.ClosedBy == PlayerPosition.NoOne &&
                !deckState.ShouldFollowSuit &&
                player.Position == trickState.PlayerTurn &&
                !trickState.Cards.Any() &&
                player.Hands.Any();
        }
    }
}