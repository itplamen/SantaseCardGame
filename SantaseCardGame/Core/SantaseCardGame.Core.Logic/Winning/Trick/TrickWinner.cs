namespace SantaseCardGame.Core.Logic.Winning.Trick
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Winning;
    using SantaseCardGame.Data.Models;

    public class TrickWinner : ITrickWinner
    {
        public PlayerPosition GetWinner(IEnumerable<KeyValuePair<PlayerPosition, Card>> cards, Card trumpCard)
        {
            Card firstPlayed = cards.First().Value;
            Card secondPlayed = cards.Last().Value;

            if (firstPlayed.Suit == secondPlayed.Suit)
            {
                if (firstPlayed.Type > secondPlayed.Type)
                {
                    return cards.First().Key;
                }

                return cards.Last().Key;
            }

            if (secondPlayed.Suit == trumpCard.Suit)
            {
                return cards.Last().Key;
            }

            return cards.First().Key;
        }
    }
}
