namespace SantaseCardGame.Data.Models
{
    public class PlayerAction
    {
        public PlayerAction(PlayerActionType type, Announce announce)
            :this(type, null, announce)
        {
        }

        public PlayerAction(PlayerActionType type, Card card, Announce announce)
        {
            Type = type;
            Card = card;
            Announce = announce;
        }

        public PlayerActionType Type { get; }

        public Card Card { get; }

        public Announce Announce { get; }
    }
}
