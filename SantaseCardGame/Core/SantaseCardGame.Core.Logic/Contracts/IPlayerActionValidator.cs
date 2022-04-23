﻿namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayerActionValidator
    {
        bool CanAnnounce(Player player);

        bool CanChangeTrump(Player player);

        bool CanCloseDeck(Player player);
    }
}
