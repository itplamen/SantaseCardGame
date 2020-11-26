namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayDifferentCard : IPlayLogic
    {
        private readonly ITrickState trickState;

        public PlayDifferentCard(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        public PlayerAction Play(Player player)
        {
            Card opponentCard = trickState.Cards.First(x => x.Key != player.Position).Value;
            
            if (player.Cards.All(x => x.Suit != opponentCard.Suit && x.Suit != trickState.TrumpCardSuit))
            {
                Card playCard =  player.Cards
                    .OrderBy(x => x.Type)
                    .FirstOrDefault();

                return new PlayerAction(PlayerActionType.PlayCard, playCard);
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}