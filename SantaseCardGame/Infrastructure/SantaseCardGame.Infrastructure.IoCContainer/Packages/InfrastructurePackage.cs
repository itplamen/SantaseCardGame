namespace SantaseCardGame.Infrastructure.IoCContainer.Packages
{
    using Microsoft.Extensions.DependencyInjection;
    
    using SantaseCardGame.Infrastructure.Contracts;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;
    using SantaseCardGame.Infrastructure.States;

    public sealed class InfrastructurePackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IGameState, GameState>();
            services.AddScoped<IDeckState, DeckState>();
            services.AddScoped<ITrickState, TrickState>();
        }
    }
}