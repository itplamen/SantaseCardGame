﻿namespace SantaseCardGame.Core.Logic.Play
{
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Utils.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class AnnounceMarriage : BaseActionPlaying
    {
        private readonly ITrickState trickState;
        private readonly ISuitFormatter suitFormatter;
        private readonly IAnnouncementChecker announcementChecker;

        public AnnounceMarriage(ITrickState trickState, ISuitFormatter suitFormatter, IAnnouncementChecker announcementChecker)
            : base(trickState)
        {
            this.trickState = trickState;
            this.suitFormatter = suitFormatter;
            this.announcementChecker = announcementChecker;
        }

        public override PlayerActionResult Play(PlayerAction playerAction, Player player)
        {
            if (ShouldPlay(playerAction, player))
            {
                Announce announcement = announcementChecker.GetAnnouncement(player, playerAction.Card);
                player.AddAnnouncement(playerAction.Card.Suit, announcement);

                player.RemoveCard(playerAction.Card);
                trickState.AddCard(playerAction.Card, player.Position);

                string message = $"Announced {(int)announcement}{suitFormatter.FormatSuit(playerAction.Card.Suit)}";
                return new PlayerActionResult(true, message);
            }

            return new PlayerActionResult(false);
        }

        protected override bool ShouldPlay(PlayerAction playerAction, Player player)
        {
            return base.ShouldPlay(playerAction, player) &&
                playerAction.Type == PlayerActionType.AnnounceCardMarriage &&
                playerAction.Announce != Announce.None &&
                playerAction.Card != null;
        }
    }
}
