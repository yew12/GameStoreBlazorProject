

using GameStore.Api.Data;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// how we connect to SQLite
var connectionString = builder.Configuration.GetConnectionString("GameStore"); // gets the connection string from appsettings.json file
builder.Services.AddSqlite<GameStoreContext>(connectionString); // dependency injection - registers context in service container

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();
