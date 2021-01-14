namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class PlayCardManager : BasePlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly ITrickState trickState;
        private readonly IEnumerable<IPlayCardValidator> playCardValidators;

        public PlayCardManager(IDeckState deckState, IGameState gameState, ITrickState trickState, IEnumerable<IPlayCardValidator> playCardValidators)
            : base(gameState, trickState)
        {
            this.deckState = deckState;
            this.gameState = gameState;
            this.trickState = trickState;
            this.playCardValidators = playCardValidators;
        }

        public override bool ShouldManage(PlayerAction playerAction, Player player)
        {
            return base.ShouldManage(playerAction, player) &&
                playerAction.Type == PlayerActionType.PlayCard && 
                playerAction.Card != null;
        }

        public override void Manage(PlayerAction playerAction, Player player)
        {
            Card opponentCard = trickState.Cards.FirstOrDefault(x => x.Key != player.Position).Value;

            if (opponentCard != null && deckState.ShouldFollowSuit)
            {
                foreach (var validator in playCardValidators)
                {
                    bool canPlay = validator.CanPlay(player, playerAction.Card, opponentCard);

                    if (!canPlay)
                    {
                        gameState.ShowMessage(player.Position, validator.Message);
                        return;
                    }
                }
            }

            player.Cards.Remove(playerAction.Card);
            trickState.AddCard(playerAction.Card, player.Position);
        }
    }
}