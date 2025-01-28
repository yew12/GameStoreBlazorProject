using GameStore.Frontend.Clients;
using GameStore.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container. + enable interactive server side rendering
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var gameStoreApiUrl = builder.Configuration["GameStoreApiUrl"] ??
    throw new Exception("GameStoreApiUrl is not set");

// Lifetime is scoped - meaning that it'll exist as long as the browser is open. 
// Every browser instance you have this will exist
builder.Services.AddHttpClient<GamesClient>(client => client.BaseAddress = new Uri(gameStoreApiUrl));
builder.Services.AddHttpClient<GenresClient>(client => client.BaseAddress = new Uri(gameStoreApiUrl));


// // One instance across services as long as app is alive.
// builder.Services.AddSingleton<GamesClient>();
// builder.Services.AddSingleton<GenresClient>();

var app = builder.Build();

// MIDDLEWARE

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS - straight transfer security - value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // only works with https
}

//app.UseHttpsRedirection(); // removes HTTPS Warning TODO: maybe change


app.UseAntiforgery(); // protects against XSS attacks

app.MapStaticAssets(); // serve html, css, and javascript files
app.MapRazorComponents<App>().AddInteractiveServerRenderMode(); // creates all razor

app.Run();
