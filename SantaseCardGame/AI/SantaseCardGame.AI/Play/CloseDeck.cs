namespace SantaseCardGame.AI.Play
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class CloseDeck : IPlayLogic
    {
        private readonly IPlayerActionValidator playerActionValidator;

        public CloseDeck(IPlayerActionValidator playerActionValidator)
        {
            this.playerActionValidator = playerActionValidator;
        }

        public PlayerAction Play(Player player)
        {
            if (playerActionValidator.CanCloseDeck(player) && ShouldClose(player))
            {
                return new PlayerAction(PlayerActionType.CloseDeck);
            }

            return new PlayerAction(PlayerActionType.None);
        }

        private bool ShouldClose(Player player)
        {
            return player.Points >= 50 ||
                    (player.Points >= 33 && player.Cards.Sum(x => (int)x.Type) >= 20) ||
                    (player.Points >= 33 && HasMarriage(player));
        }

        private bool HasMarriage(Player player)
        {
            IEnumerable<Card> marriages = player.Cards
                .Where(x => x.Type == CardType.Queen || x.Type == CardType.King)
                .GroupBy(x => x.Suit)
                .Where(x => x.Count() == 2)
                .SelectMany(x => x);

            return marriages.Any();
        }
    }
}