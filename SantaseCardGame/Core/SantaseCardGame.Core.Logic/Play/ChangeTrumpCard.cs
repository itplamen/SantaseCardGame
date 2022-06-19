namespace SantaseCardGame.Core.Logic.Play
{
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class ChangeTrumpCard : BaseActionPlaying
    {
        private readonly IDeckState deckState;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCard(IGameState gameState, ITrickState trickState, IDeckState deckState, IPlayerActionValidator playerActionValidator) 
            : base(gameState, trickState)
        {
            this.deckState = deckState;
            this.playerActionValidator = playerActionValidator;
        }

        public override PlayerActionResult Play(PlayerAction playerAction, Player player)
        {
            if (ShouldPlay(playerAction, player))
            {
                Card nineOfTrumpsCard = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == playerAction.Card.Suit);
                
                if (nineOfTrumpsCard != null)
                {
                    int nineOfTrumpsIndex = player.GetCardPosition(nineOfTrumpsCard.Type, nineOfTrumpsCard.Suit);

                    player.RemoveCard(nineOfTrumpsCard);
                    player.AddCard(playerAction.Card, nineOfTrumpsIndex);

                    deckState.ExchangeTrumpCardForNineOfTrumps(nineOfTrumpsCard);

                    return new PlayerActionResult(true, "Trump card changed");
                }
            }

            return new PlayerActionResult(false);
        }

        protected override bool ShouldPlay(PlayerAction playerAction, Player player)
        {
            return base.ShouldPlay(playerAction, player) &&
                playerActionValidator.CanChangeTrump(player) &&
                playerAction.Type == PlayerActionType.ChangeTrumpCard &&
                playerAction.Card != null;
        }
    }
}
