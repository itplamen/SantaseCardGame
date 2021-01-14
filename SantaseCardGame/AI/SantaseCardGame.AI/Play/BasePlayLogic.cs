namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public abstract class BasePlayLogic : IPlayLogic
    {
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
    }
}