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
            int nineOfTrumpsIndex = player.Cards.FindIndex(x => x.Type == nineOfTrumpsCard.Type && x.Suit == nineOfTrumpsCard.Suit);

            player.Cards.Remove(nineOfTrumpsCard);
            player.Cards.Insert(nineOfTrumpsIndex, trumpCard);

            deck.Cards.Remove(trumpCard);
            deck.Cards.Add(nineOfTrumpsCard);
            deck.TrumpCard = nineOfTrumpsCard;
        }
    }
}
