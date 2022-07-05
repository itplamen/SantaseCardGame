namespace SantaseCardGame.Core.Logic.Exchanging
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class TrumpCardExchanger : ITrumpCardExchanger
    {
        public void Exchange(Card trumpCard, Deck deck, Player player)
        {
            Card nineOfTrumpsCard = player.Cards.First(x => x.Type == CardType.Nine && x.Suit == trumpCard.Suit);
            int nineOfTrumpsIndex = player.GetCardPosition(nineOfTrumpsCard.Type, nineOfTrumpsCard.Suit);

            player.RemoveCard(nineOfTrumpsCard);
            player.AddCard(trumpCard, nineOfTrumpsIndex);

            deck.RemoveCard(trumpCard);
            deck.AddCard(nineOfTrumpsCard);
        }
    }
}
