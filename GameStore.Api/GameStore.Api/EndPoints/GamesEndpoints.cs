using System;
using GameStore.Api.Contracts;
using GameStore.Api.Data;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation(); // Nuget API Extension - allows us to recognize the parameter validation in CreateGameDto.cs

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
        await dbContext.Games
                        .Include(game => game.Genre) // This grabs the Genre so it's not empty in the DTO
                        .Select(game => game.ToGameSummaryDto())
                        .AsNoTracking()
                        .ToListAsync()); // As no tracking of the return entities, improve performance

        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            // first tries to find in memory, if not then will try in db
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost(
            "/",
            async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = newGame.ToEntity();
                game.Genre = dbContext.Genres.Find(newGame.GenreId);

                dbContext.Games.Add(game); // Adds to database the new game

                await dbContext.SaveChangesAsync(); // transforms all changes to sql to insert into database

                // standard is to return 201 response
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
            }
        );

        // PUT /games
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                // could just create a new resource or just return not found up to you.
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();

            // convention for update is NoContent
            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync(); // batch delete

            return Results.NoContent();
        });

        return group;
    }

}
