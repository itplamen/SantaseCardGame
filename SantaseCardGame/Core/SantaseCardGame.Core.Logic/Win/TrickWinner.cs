namespace SantaseCardGame.Core.Logic.Win
{
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class TrickWinner : ITrickWinner
    {
        public PlayerPosition GetWinner(Card firstPlayerCard, Card secondPlayerCard, CardSuit trumpSuit)
        {
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