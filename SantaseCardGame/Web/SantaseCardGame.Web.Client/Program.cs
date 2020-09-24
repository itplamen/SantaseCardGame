namespace SantaseCardGame.Web.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Infrastructure.States;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Core.Logic.Deal;
    using SantaseCardGame.Core.Logic.Play;
    using SantaseCardGame.Core.Logic.Shuffle;
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

            await builder.Build().RunAsync();
        }
    }
}