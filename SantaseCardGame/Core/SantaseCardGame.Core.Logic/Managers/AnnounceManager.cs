namespace SantaseCardGame.Core.Logic.Managers
{
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class AnnounceManager : IPlayerActionManager
    {
        private readonly ITrickState trickState;
        private readonly IAnnounceCardProvider announceCardProvider;

        public AnnounceManager(ITrickState trickState, IAnnounceCardProvider announceCardProvider)
        {
            this.trickState = trickState;
            this.announceCardProvider = announceCardProvider;
        }

        public bool ShouldManage(PlayerAction playerAction)
        {
            return playerAction.Type == PlayerActionType.Announce && 
                playerAction.Announce != Announce.None &&
                playerAction.Card != null;
        }

        public void Manage(PlayerAction playerAction, Player player)
        {
            Announce announce = announceCardProvider.GetAnnounce(player, playerAction.Card).Announce;

            if (announce != Announce.None)
            {
                player.Announcements.Add(playerAction.Card.Suit, announce);

                // TODO: notify
            }

            player.Cards.Remove(playerAction.Card);
            trickState.AddCard(playerAction.Card, player.Position);
        }
    }
}