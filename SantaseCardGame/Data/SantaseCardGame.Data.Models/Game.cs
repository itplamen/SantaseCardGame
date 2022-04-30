namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Game
    {
        private readonly List<Player> players = new List<Player>();

        private readonly List<Round> rounds = new List<Round>();

        public string Id { get; set; }

        public GameType Type { get; set; }

        public Deck Deck { get; set; }

        public IEnumerable<Player> Players => players;

        public IEnumerable<Round> Rounds => rounds;

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void AddRound(Round round)
        {
            rounds.Add(round);
        }
    }
}
