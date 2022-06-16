namespace SantaseCardGame.Core.Logic.Winning.Trick
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Winning;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class TrickWinner : ITrickWinner
    {
        private readonly IDeckState deckState;

        public TrickWinner(IDeckState deckState)
        {
            this.deckState = deckState;
        }

        public PlayerPosition GetWinner(IEnumerable<KeyValuePair<PlayerPosition, Card>> cards)
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

            if (secondPlayed.Suit == deckState.TrumpCard.Suit)
            {
                return cards.Last().Key;
            }

            return cards.First().Key;
        }
    }
}
