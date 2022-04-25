namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        private readonly List<Card> cards = new List<Card>();

        private readonly List<IEnumerable<Card>> hands = new List<IEnumerable<Card>>();

        private readonly Dictionary<CardSuit, Announce> announcements = new Dictionary<CardSuit, Announce>();

        public int Points => CalculatePoints();

        public int BonusPoints { get; set; }

        public string Username { get; set; }

        public PlayerPosition Position { get; set; }

        public IEnumerable<Card> Cards => cards;

        public IEnumerable<IEnumerable<Card>> Hands => hands;

        public IEnumerable<KeyValuePair<CardSuit, Announce>> Announcements => announcements;

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

        public void AddHand(IEnumerable<Card> hand)
        {
            hands.Add(hand);
        }

        public void AddAnnouncement(CardSuit suit, Announce announcement)
        {
            announcements.Add(suit, announcement);
        }

        public void Clear()
        {
            cards.Clear();
            hands.Clear();
            announcements.Clear();
        }

        private int CalculatePoints()
        {
            if (Hands.Any())
            {
                int handsSum = Hands.SelectMany(x => x).Sum(x => (int)x.Type);
                int announcementsSum = announcements.Any() ? announcements.Values.Sum(x => (int)x) : 0;

                return handsSum + announcementsSum + BonusPoints;
            }

            return 0;
        }
    }
}
