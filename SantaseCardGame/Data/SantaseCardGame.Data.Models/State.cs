namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class State
    {
        public string GameId { get; set; }

        public bool ShouldFollowSuit { get; set; }

        public PlayerPosition PlayerTurn { get; set; }

        public IEnumerable<KeyValuePair<PlayerPosition, Card>> TrickCards { get; set; } = new Dictionary<PlayerPosition, Card>();
    }
}
