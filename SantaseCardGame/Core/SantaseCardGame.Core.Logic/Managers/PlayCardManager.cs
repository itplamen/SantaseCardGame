namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayCardManager : IPlayerActionManager
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayCardManager(IDeckState deckState, ITrickState trickState)
        {
            this.deckState = deckState;
            this.trickState = trickState;
        }

        public bool ShouldManage(PlayerAction playerAction)
        {
            return playerAction.Type == PlayerActionType.PlayCard && playerAction.Card != null;
        }

        public void Manage(PlayerAction playerAction, Player player)
        {
            if (player.Position == trickState.PlayerTurn)
            {
                Card opponentCard = trickState.Cards.FirstOrDefault(x => x.Key != player.Position).Value;

                if (opponentCard != null && deckState.ShouldFollowSuit)
                {
                    IEnumerable<Card> sameSuitCards = player.Cards.Where(x => x.Suit == opponentCard.Suit);

                    if ((sameSuitCards.Any(x => x.Type > opponentCard.Type) && playerAction.Card.Type < opponentCard.Type) || 
                        (sameSuitCards.Any() && playerAction.Card.Suit != opponentCard.Suit) || 
                        (!sameSuitCards.Any() && player.Cards.Any(x => x.Suit == trickState.TrumpCardSuit) && playerAction.Card.Suit != trickState.TrumpCardSuit))
                    {
                        // TODO: Notify
                        return;
                    }    
                }

                player.Cards.Remove(playerAction.Card);
                trickState.AddCard(playerAction.Card, player.Position);
            }
        }
    }
}