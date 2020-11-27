namespace SantaseCardGame.AI.Play
{
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayHigherCard : BasePlayLogic
    {
        public PlayHigherCard(ITrickState trickState) 
            : base(trickState)
        {
        }

        protected override PlayerAction PlayLogic(Player player)
        {
            Card card = player.Cards
                .Where(x => x.Suit == OpponentCard.Suit)
                .OrderByDescending(x => x.Type)
                .FirstOrDefault(x => x.Type > OpponentCard.Type);

            return new PlayerAction(PlayerActionType.PlayCard, card);
        }
    }
}