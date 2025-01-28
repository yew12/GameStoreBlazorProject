using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

/// <summary>
/// Game Store Client for handling our api requests
/// </summary>
/// <param name="httpClient">Receiving from dependency injection done in program.cs</param>
public class GamesClient(HttpClient httpClient)
{
    public async Task<GameSummary[]> GetGamesAsync()
        => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];

    /// <summary>
    /// Adds new game to backend using our api
    /// </summary>
    /// <param name="game"></param>
    public async Task AddGameAsync(GameDetails game) 
        => await httpClient.PostAsJsonAsync("games", game);

    /// <summary>
    /// Gets specific game from backend using our api
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<GameDetails> GetGameAsync(int id)
        => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") ?? 
            throw new Exception("Could not get game");

    /// <summary>
    /// Updates a game using api
    /// </summary>
    /// <param name="updatedGame"></param>
    public async Task UpdateGameAsync(GameDetails updatedGame) 
        => await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);
    

    /// <summary>
    /// deletes game using a api
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteGameAsync(int id) 
        => await httpClient.DeleteAsync($"games/{id}");
}
