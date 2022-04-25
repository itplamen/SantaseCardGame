namespace SantaseCardGame.Core.Logic.Validators.PlayerActions
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayerActionValidator : IPlayerActionValidator
    {
        private readonly IGameRules gameRules;
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayerActionValidator(IGameRules gameRules, IDeckState deckState, ITrickState trickState)
        {
            this.gameRules = gameRules;
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
            return deckState.CardsLeft >= gameRules.DeckMinCardsBeforeClosing &&
                deckState.ClosedBy == PlayerPosition.None &&
                !deckState.ShouldFollowSuit &&
                player.Position == trickState.PlayerTurn &&
                !trickState.Cards.Any() &&
                player.Hands.Any();
        }
    }
}
