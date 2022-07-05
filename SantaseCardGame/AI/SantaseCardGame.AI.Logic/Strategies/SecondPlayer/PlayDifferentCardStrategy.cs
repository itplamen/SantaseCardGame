namespace SantaseCardGame.AI.Logic.Strategies.SecondPlayer
{
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayDifferentCardStrategy : BasePlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;

        public PlayDifferentCardStrategy(IDeckState deckState, ITrickState trickState)
        {
            this.deckState = deckState;
            this.trickState = trickState;
        }

        protected override PlayerAction SelectStrategy(Player player)
        {
            var opponentCard = trickState.Cards.First(x => x.Key != player.Position).Value;

            if (player.Cards.All(x => x.Suit != opponentCard.Suit))
            {
                Card card = player.Cards
                    .Where(x => x.Suit != deckState.TrumpCard.Suit)
                    .OrderBy(x => x.Type)
                    .FirstOrDefault();

                return new PlayerAction(PlayerActionType.PlayCard, card);
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}
