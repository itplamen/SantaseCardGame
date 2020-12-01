namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Game
    {
        public Deck Deck { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();

        public List<Round> Rounds { get; set; } = new List<Round>();
    }
}