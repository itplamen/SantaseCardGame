using SantaseCardGame.Core.Infrastructure.Contracts;
using SantaseCardGame.Core.Logic.Contracts;
using SantaseCardGame.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaseCardGame.Core.Logic.Validators
{
    public class PlayerActionValidator : IPlayerActionValidator
    {
        private readonly ITrickState trickState;

        public bool CanAnnounce(Player player)
        {
            return player.Position == trickState.PlayerTurn &&
                !trickState.Cards.Any() &&
                player.Hands.Any();
        }
    }
}
