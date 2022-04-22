namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        public int Points => CalculatePoints();

        public int BonusPoints { get; set; }

        public string Username { get; set; }

        public PlayerPosition Position { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>();

        public ICollection<IEnumerable<Card>> Hands { get; set; } = new List<IEnumerable<Card>>();

        public Dictionary<CardSuit, Announce> Announcements { get; set; } = new Dictionary<CardSuit, Announce>();

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
