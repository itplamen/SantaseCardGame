﻿namespace SantaseCardGame.AI.Logic.Strategies.FirstPlayer
{
    using System.Linq;

    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class ChangeTrumpCardStrategy : IPlayerActionStrategy
    {
        private readonly IGameState gameState;
        private readonly IStorage<Game> gameStorage;
        private readonly IPlayerActionValidator playerActionValidator;

        public ChangeTrumpCardStrategy(IGameState gameState, IStorage<Game> gameStorage, IPlayerActionValidator playerActionValidator)
        {
            this.gameState = gameState;
            this.gameStorage = gameStorage;
            this.playerActionValidator = playerActionValidator;
        }

        public PlayerAction ChooseAction(Player player)
        {
            if (playerActionValidator.CanChangeTrump(player))
            {
                Game game = gameStorage.Get(gameState.CurrentGameId);
                Card nineOfTrumps = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == game.Deck.TrumpCard.Suit);

                if (nineOfTrumps != null)
                {
                    return new PlayerAction(PlayerActionType.ChangeTrumpCard, game.Deck.TrumpCard);
                }
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}
