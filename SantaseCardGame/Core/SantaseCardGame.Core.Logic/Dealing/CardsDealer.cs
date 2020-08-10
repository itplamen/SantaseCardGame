namespace SantaseCardGame.Core.Logic.Dealing
{
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class CardsDealer : ICardsDealer
    {
        private const int NUMBER_OF_CARDS = 6;

        public void Deal(Deck deck, Player firstPlayer, Player secondPlayer)
        {
            DealCards(deck, firstPlayer);
            DealCards(deck, secondPlayer);

            Card trumpCard = GetNextCard(deck);
            deck.TrumpCard = trumpCard;
        }

        private void DealCards(Deck deck, Player player)
        {
            for (int i = 1; i <= NUMBER_OF_CARDS; i++)
            {
                Card card = GetNextCard(deck);
                player.Cards.Add(card);
            }
        }

        private Card GetNextCard(Deck deck)
        {
            Card card = deck.Cards[deck.Cards.Count - 1];
            deck.Cards.RemoveAt(deck.Cards.Count - 1);

            return card;
        }
    }
}