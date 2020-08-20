namespace SantaseCardGame.Core.Logic.Win
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class TrickWinner : ITrickWinner
    {
        public PlayerPosition GetWinner(IEnumerable<Card> trickCards, CardSuit trumpSuit)
        {
            Card firstPlayerCard = trickCards.First();
            Card secondPlayerCard = trickCards.Last();

            if (firstPlayerCard.Suit == secondPlayerCard.Suit)
            {
                return firstPlayerCard.Type > secondPlayerCard.Type ? PlayerPosition.First : PlayerPosition.Second;
            }

            if (secondPlayerCard.Suit == trumpSuit)
            {
                return PlayerPosition.Second;
            }

            return PlayerPosition.First;
        }
    }
}