﻿namespace SantaseCardGame.Infrastructure.States
{
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class DeckState : IDeckState
    {
        public bool ShouldFollowSuit { get; set; }

        public CardSuit TrumpCardSuit { get; set; }
    }
}