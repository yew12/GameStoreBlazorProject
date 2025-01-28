using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    /// <summary>
    /// Migrates db on startup 
    /// </summary>
    /// <param name="app"></param>
    public static async Task MigrateDbAsync(this WebApplication app) 
    {
        // use to request service container to get an instance of the services that have been registered 
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync(); // call migrate method
    }
}
