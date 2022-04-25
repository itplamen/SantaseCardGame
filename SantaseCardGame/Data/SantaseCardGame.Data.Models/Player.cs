namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        private readonly List<Card> cards = new List<Card>();

        public int Points => CalculatePoints();

        public int BonusPoints { get; set; }

        public string Username { get; set; }

        public PlayerPosition Position { get; set; }

        public IEnumerable<Card> Cards => cards;

        public ICollection<IEnumerable<Card>> Hands { get; set; } = new List<IEnumerable<Card>>();

        public Dictionary<CardSuit, Announce> Announcements { get; set; } = new Dictionary<CardSuit, Announce>();

        public void AddCard(Card card, int? index = null)
        {
            if (index.HasValue && index.Value >= 0)
            {
                cards.Insert(index.Value, card);
            }
            else
            {
                cards.Add(card);
            }
        }

        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        public int GetCardPosition(CardType type, CardSuit suit)
        {
            return cards.FindIndex(x => x.Type == type && x.Suit == suit);
        }

        private int CalculatePoints()
        {
            if (Hands.Any())
            {
                int handsSum = Hands.SelectMany(x => x).Sum(x => (int)x.Type);
                int announcementsSum = Announcements.Any() ? Announcements.Values.Sum(x => (int)x) : 0;

                return handsSum + announcementsSum + BonusPoints;
            }

            return 0;
        }
    }
}
