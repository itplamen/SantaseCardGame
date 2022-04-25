namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Game
    {
        private readonly List<Player> players = new List<Player>();

        public string Id { get; set; }

        public GameType Type { get; set; }

        public Deck Deck { get; set; }

        public IEnumerable<Player> Players => players;

        public ICollection<Round> Rounds { get; set; } = new List<Round>();

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
    }
}
