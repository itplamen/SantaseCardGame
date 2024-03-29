﻿namespace SantaseCardGame.Infrastructure.IoCContainer.IoCPackages
{
    using Microsoft.Extensions.DependencyInjection;
    
    using SantaseCardGame.Core.Engine;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Logic.Announcements;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Contracts.Validators;
    using SantaseCardGame.Core.Logic.Contracts.Winning;
    using SantaseCardGame.Core.Logic.Deal;
    using SantaseCardGame.Core.Logic.Exchanging;
    using SantaseCardGame.Core.Logic.GameStates;
    using SantaseCardGame.Core.Logic.Play;
    using SantaseCardGame.Core.Logic.Validators.CardPlaying;
    using SantaseCardGame.Core.Logic.Validators.PlayerActions;
    using SantaseCardGame.Core.Logic.Winning.Round;
    using SantaseCardGame.Core.Logic.Winning.Trick;
    using SantaseCardGame.Core.Utils;
    using SantaseCardGame.Core.Utils.Contracts;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;

    public sealed class CorePackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IGameEngine, GameEngine>();
            services.AddSingleton<IGameLoaderManager, GameLoaderManager>();

            services.AddSingleton<IAnnouncementChecker, AnnouncementChecker>();
            services.AddSingleton<ICardsDealer, CardsDealer>();
            services.AddSingleton<ITrumpCardExchanger, TrumpCardExchanger>();

            services.AddSingleton<IGameStateHandler, StartOfRound>();
            services.AddSingleton<IGameStateHandler, EndOfTrickHandler>();
            services.AddSingleton<IGameStateHandler, MainGameHandler>();

            services.AddSingleton<IActionPlaying, AnnounceMarriage>();
            services.AddSingleton<IActionPlaying, ChangeTrumpCard>();
            services.AddSingleton<IActionPlaying, CloseDeck>();
            services.AddSingleton<IActionPlaying, PlayCard>();

            services.AddSingleton<ICardPlayableValidator, PlaySameSuitCardValidator>();
            services.AddSingleton<ICardPlayableValidator, PlayHigherCardValidator>();          
            services.AddSingleton<ICardPlayableValidator, PlayTrumpCardValidator>();

            services.AddSingleton<IPlayerActionValidator, PlayerActionValidator>();

            services.AddSingleton<IRoundWinner, DeckClosedRoundWinner>();
            services.AddSingleton<IRoundWinner, DeckNotClosedRoundWinner>();
            services.AddSingleton<ITrickWinner, TrickWinner>();

            services.AddSingleton<ISuitFormatter, SuitFormatter>();
        }
    }
}
