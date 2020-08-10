namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ITrickWinner
    {
        PlayerPosition GetWinner(Card firstPlayerCard, Card secondPlayerCard, CardSuit trumpSuit);
    }
}