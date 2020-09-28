namespace SantaseCardGame.Core.Logic.Deal
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class CardsDealer : ICardsDealer
    {
        private const int NUMBER_OF_CARDS = 6;

        public void Deal(Deck deck, Player firstPlayer, Player secondPlayer)
        {
            DealCards(deck, firstPlayer);
            DealCards(deck, secondPlayer);

            deck.TrumpCard = deck.Cards.Last();
        }

        private void DealCards(Deck deck, Player player)
        {
            for (int i = 1; i <= NUMBER_OF_CARDS; i++)
            {
                Card card = deck.GetNextCard();
                player.Cards.Add(card);
            }
        }
    }
}