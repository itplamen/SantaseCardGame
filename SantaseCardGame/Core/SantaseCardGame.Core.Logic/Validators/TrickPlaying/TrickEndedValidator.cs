namespace SantaseCardGame.Core.Logic.Validators.TrickPlaying
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class TrickEndedValidator : ITrickEndedValidator
    {
        private readonly IGameRules gameRules;
        private readonly ITrickState trickState;

        public TrickEndedValidator(IGameRules gameRules, ITrickState trickState)
        {
            this.gameRules = gameRules;
            this.trickState = trickState;
        }

        public bool HasEnded()
        {
            return trickState.Cards.Count() == gameRules.TrickCardsCount;
        }
    }
}
