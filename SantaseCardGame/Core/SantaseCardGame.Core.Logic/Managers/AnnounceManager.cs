namespace SantaseCardGame.Core.Logic.Managers
{
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class AnnounceManager : BasePlayerActionManager
    {
        private readonly IGameState gameState;
        private readonly ITrickState trickState;
        private readonly IAnnounceCardProvider announceCardProvider;

        public AnnounceManager(IGameState gameState, ITrickState trickState, IAnnounceCardProvider announceCardProvider)
            : base(gameState, trickState)
        {
            this.gameState = gameState;
            this.trickState = trickState;
            this.announceCardProvider = announceCardProvider;
        }

        public override bool ShouldManage(PlayerAction playerAction, Player player)
        {
            return base.ShouldManage(playerAction, player) && 
                playerAction.Type == PlayerActionType.Announce && 
                playerAction.Announce != Announce.None &&
                playerAction.Card != null;
        }

        public override void Manage(PlayerAction playerAction, Player player)
        {
            Announce announce = announceCardProvider.GetAnnounce(player, playerAction.Card).Announce;

            if (announce != Announce.None)
            {
                player.Announcements.Add(playerAction.Card.Suit, announce);
                gameState.ShowMessage(player.Position, $"Announced {(int)announce}");
            }

            player.Cards.Remove(playerAction.Card);
            trickState.AddCard(playerAction.Card, player.Position);
        }
    }
}