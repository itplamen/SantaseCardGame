namespace SantaseCardGame.Core.Logic.Play
{
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class CloseDeck : BaseActionPlaying
    {
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly IGameStorage gameStorage;
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeck(IGameState gameState, ITrickState trickState, IDeckState deckState, IGameStorage gameStorage, IPlayerActionValidator playerActionValidator)
            : base(gameState, trickState)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.gameStorage = gameStorage;
            this.playerActionValidator = playerActionValidator;
        }

        public override PlayerActionResult Play(PlayerAction playerAction, Player player)
        {
            if (ShouldPlay(playerAction, player))
            {
                Game game = gameStorage.Get(gameState.CurrentGameId);
                game.Deck.ClosedBy = player.Position;

                deckState.ShouldFollowSuit = true;

                return new PlayerActionResult(true, "Deck closed");
            }

            return new PlayerActionResult(false);
        }

        protected override bool ShouldPlay(PlayerAction playerAction, Player player)
        {
            return base.ShouldPlay(playerAction, player) &&
                playerActionValidator.CanCloseDeck(player) &&
                playerAction.Type == PlayerActionType.CloseDeck;
        }
    }
}
