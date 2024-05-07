using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SchoolMeetings.Presentation;
using SchoolMeetings.Presentation.DependencyInjection;
using SchoolMeetings.Presentation.Identity;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CookieHandler>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();

builder.Services.AddScoped(
    sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient(
        "SchoolMeetingsApi",
        opt => opt.BaseAddress = new Uri("https://localhost:7131"))
    .AddHttpMessageHandler<CookieHandler>();


builder.Services.AddViewModels();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();