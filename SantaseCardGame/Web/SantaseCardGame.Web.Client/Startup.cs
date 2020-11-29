namespace SantaseCardGame.Web.Client
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.AI.Play.First;
    using SantaseCardGame.AI.Play.Second;
    using SantaseCardGame.Core.Engine;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Infrastructure.States;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Deal;
    using SantaseCardGame.Core.Logic.Managers;
    using SantaseCardGame.Core.Logic.Providers;
    using SantaseCardGame.Core.Logic.Shuffle;
    using SantaseCardGame.Core.Logic.Validators;
    using SantaseCardGame.Core.Logic.Win;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Providers;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<ICardsProvider, CardsProvider>();
            services.AddScoped<ITrickState, TrickState>();
            services.AddScoped<IDeckState, DeckState>();
            services.AddTransient<ITrickWinner, TrickWinner>();
            services.AddTransient<ICardsShuffler, CardsShuffler>();
            services.AddTransient<ICardsDealer, CardsDealer>();
            services.AddTransient<IGameEngine, GameEngine>();
            services.AddTransient<IPlayerActionManager, AnnounceManager>();
            services.AddTransient<IPlayerActionManager, ChangeTrumpCardManager>();
            services.AddTransient<IPlayerActionManager, CloseDeckManager>();
            services.AddTransient<IPlayerActionManager, PlayCardManager>();
            services.AddTransient<IAnnounceCardProvider, AnnounceCardProvider>();
            services.AddTransient<IPlayLogic, AnnounceMarriage>();
            services.AddTransient<IPlayLogic, ChangeTrumpCard>();
            services.AddTransient<IPlayLogic, CloseDeck>();
            services.AddTransient<IPlayLogic, PlayHigherCard>();
            services.AddTransient<IPlayLogic, PlayLowerCard>();
            services.AddTransient<IPlayLogic, PlayTrumpCard>();
            services.AddTransient<IPlayLogic, PlayDifferentCard>();
            services.AddTransient<IPlayerActionValidator, PlayerActionValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}