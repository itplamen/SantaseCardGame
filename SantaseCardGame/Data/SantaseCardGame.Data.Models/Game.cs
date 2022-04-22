namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Game
    {
        public string Id { get; set; }

        public GameType Type { get; set; }

        public Deck Deck { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();

        public ICollection<Round> Rounds { get; set; } = new List<Round>();
    }
}
