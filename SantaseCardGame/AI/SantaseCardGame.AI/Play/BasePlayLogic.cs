namespace SantaseCardGame.AI.Play
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public abstract class BasePlayLogic : IPlayLogic
    {
        private const int MARRIAGE_CARDS_COUNT = 2;

        private readonly ITrickState trickState;

        protected BasePlayLogic(ITrickState trickState)
        {
            this.trickState = trickState;
        }

        protected Card OpponentCard { get; private set; }

        public PlayerAction Play(Player player)
        {
            OpponentCard = trickState.Cards.FirstOrDefault(x => x.Key != player.Position).Value;
            PlayerAction playLogic = PlayLogic(player);

            if (playLogic.Type != PlayerActionType.None && playLogic.Card != null)
            {
                return playLogic;
            }

            return new PlayerAction(PlayerActionType.None);
        }

        protected abstract PlayerAction PlayLogic(Player player);

        protected IEnumerable<Card> GetMarriages(Player player)
        {
            return player.Cards
                .Where(x => x.Type == CardType.Queen || x.Type == CardType.King)
                .GroupBy(x => x.Suit)
                .Where(x => x.Count() == MARRIAGE_CARDS_COUNT)
                .SelectMany(x => x);
        }
    }
}