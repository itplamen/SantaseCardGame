using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using SantaseCardGame.Client.Web;
using SantaseCardGame.Data;
using SantaseCardGame.Data.Contracts;
using SantaseCardGame.Data.Models;
using SantaseCardGame.Infrastructure.IoCContainer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.RegisterGameServices();

var host = builder.Build();
var localStorage = host.Services.GetRequiredService<LocalGameStorage>();
var stateStorage = host.Services.GetRequiredService<IStorage<State>>();

await localStorage.ClearExpiredData();
await stateStorage.ClearExpiredData();

await host.RunAsync();