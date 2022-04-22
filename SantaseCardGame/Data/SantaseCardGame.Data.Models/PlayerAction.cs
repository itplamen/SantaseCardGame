namespace SantaseCardGame.Data.Models
{
    public class PlayerAction
    {
        public PlayerAction()
        {
        }

        public PlayerAction(PlayerActionType type)
            : this(type, null, Announce.None)
        {
        }

        public PlayerAction(PlayerActionType type, Announce announce)
            : this(type, null, announce)
        {
        }

        public PlayerAction(PlayerActionType type, Card card)
            : this(type, card, Announce.None)
        {
        }

        public PlayerAction(PlayerActionType type, Card card, Announce announce)
        {
            Type = type;
            Card = card;
            Announce = announce;
        }

        public PlayerActionType Type { get; set; }

        public Card Card { get; set; }

        public Announce Announce { get; set; }
    }
}
