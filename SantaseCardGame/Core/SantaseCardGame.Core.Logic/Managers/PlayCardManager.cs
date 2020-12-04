namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class PlayCardManager : BasePlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly IGameState gameState;
        private readonly ITrickState trickState;

        public PlayCardManager(IDeckState deckState, IGameState gameState, ITrickState trickState)
            : base(trickState)
        {
            this.deckState = deckState;
            this.gameState = gameState;
            this.trickState = trickState;
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
                IEnumerable<Card> sameSuitCards = player.Cards.Where(x => x.Suit == opponentCard.Suit);

                if ((sameSuitCards.Any(x => x.Type > opponentCard.Type) && playerAction.Card.Type < opponentCard.Type) ||
                    (sameSuitCards.Any() && playerAction.Card.Suit != opponentCard.Suit) ||
                    (!sameSuitCards.Any() && player.Cards.Any(x => x.Suit == deckState.TrumpCard.Suit) && playerAction.Card.Suit != deckState.TrumpCard.Suit))
                {
                    gameState.ShowMessage("Cant play");

                    return;
                }
            }

            player.Cards.Remove(playerAction.Card);
            trickState.AddCard(playerAction.Card, player.Position);
        }
    }
}