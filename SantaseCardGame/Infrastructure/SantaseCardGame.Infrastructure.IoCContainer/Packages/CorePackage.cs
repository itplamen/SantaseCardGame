namespace SantaseCardGame.Infrastructure.IoCContainer.Packages
{
    using Microsoft.Extensions.DependencyInjection;

    using SantaseCardGame.Core.Engine;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Deal;
    using SantaseCardGame.Core.Logic.Managers;
    using SantaseCardGame.Core.Logic.Providers;
    using SantaseCardGame.Core.Logic.Validators;
    using SantaseCardGame.Core.Logic.Win;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;

    public sealed class CorePackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ITrickWinner, TrickWinner>();
            services.AddTransient<ICardsDealer, CardsDealer>();
            services.AddTransient<IGameEngine, GameEngine>();
            services.AddTransient<IPlayerActionManager, AnnounceManager>();
            services.AddTransient<IPlayerActionManager, ChangeTrumpCardManager>();
            services.AddTransient<IPlayerActionManager, CloseDeckManager>();
            services.AddTransient<IPlayerActionManager, PlayCardManager>();
            services.AddTransient<IPlayCardValidator, PlaySameSuitCardValidator>();
            services.AddTransient<IPlayCardValidator, PlayHigherCardValidator>();
            services.AddTransient<IPlayCardValidator, PlayTrumpCardValidator>();
            services.AddTransient<ITrickManager, TrickManager>();
            services.AddTransient<IRoundManager, RoundManager>();
            services.AddTransient<IStatesManager, StatesManager>();
            services.AddTransient<IAnnounceProvider, AnnounceProvider>();
            services.AddTransient<IPlayerActionValidator, PlayerActionValidator>();
            services.AddTransient<IRoundWinner, DeckClosedRoundWinner>();
            services.AddTransient<IRoundWinner, DeckNotClosedRoundWinner>();
        }
    }
}