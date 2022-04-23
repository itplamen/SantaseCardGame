using System;
using System.Collections.Generic;
using System.Text;

namespace SantaseCardGame.Core.Logic.Contracts
{
    public interface IGameRules
    {
        int RoundInitialCardsCount { get; }
    }
}
