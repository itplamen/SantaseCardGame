namespace SantaseCardGame.AI.Play.Second
{
    using System.Linq;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class PlayDifferentCard : BasePlayLogic
    {
        private readonly IDeckState deckState;

        public PlayDifferentCard(ITrickState trickState, IDeckState deckState)
            : base(trickState)
        {
            this.deckState = deckState;
        }

        protected override PlayerAction PlayLogic(Player player)
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