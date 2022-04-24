namespace SantaseCardGame.AI.Logic.Strategies.SecondPlayer
{
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayDifferentCardStrategy : BasePlayerActionStrategy
    {
        private readonly IDeckState deckState;

        public PlayDifferentCardStrategy(ITrickState trickState, IDeckState deckState)
            : base(trickState)
        {
            this.deckState = deckState;
        }

        protected override PlayerAction PlayStrategy(Player player)
        {
            if (player.Cards.All(x => x.Suit != OpponentCard.Suit))
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
