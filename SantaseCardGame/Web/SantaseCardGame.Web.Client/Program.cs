namespace SantaseCardGame.Web.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.AI.Decisions.PlayCard;
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

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<ICardsProvider, CardsProvider>();
            builder.Services.AddSingleton<ITrickState, TrickState>();
            builder.Services.AddSingleton<IDeckState, DeckState>();
            builder.Services.AddTransient<ITrickWinner, TrickWinner>();
            builder.Services.AddTransient<ICardsShuffler, CardsShuffler>();
            builder.Services.AddTransient<ICardsDealer, CardsDealer>();
            builder.Services.AddTransient<IGameManager, GameManager>();
            builder.Services.AddTransient<IPlayerManager, PlayerManager>();
            builder.Services.AddTransient<IAnnounceCardProvider, AnnounceCardProvider>();
            builder.Services.AddTransient<IPlayCardDecision, PlayHigherCard>();
            builder.Services.AddTransient<IPlayCardDecision, PlayLowerCard>();
            builder.Services.AddTransient<IPlayCardDecision, PlayTrumpCard>();
            builder.Services.AddTransient<IPlayCardDecision, PlayDifferentCard>();
            builder.Services.AddTransient<IPlayerActionValidator, PlayerActionValidator>();

            await builder.Build().RunAsync();
        }
    }
}