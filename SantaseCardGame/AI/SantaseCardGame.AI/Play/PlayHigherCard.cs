namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayHigherCard : IPlayLogic
    {
        private readonly ITrickState trickState;

        public PlayHigherCard(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        public PlayerAction Play(Player player)
        {
            Card opponentCard = trickState.Cards.First(x => x.Key != player.Position).Value;
            Card playCard = player.Cards
                .Where(x => x.Suit == opponentCard.Suit)
                .OrderByDescending(x => x.Type)
                .FirstOrDefault(x => x.Type > opponentCard.Type);

            if (playCard != null)
            {
                return new PlayerAction(PlayerActionType.PlayCard, playCard);
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}