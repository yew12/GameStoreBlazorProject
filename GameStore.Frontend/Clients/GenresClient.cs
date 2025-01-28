using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

/// <summary>
/// 
/// </summary>
/// <param name="httpClient">Receiving from dependency injection done in program.cs</param>
public class GenresClient(HttpClient httpClient)
{
    /// <summary>
    /// Gets Genres from our genres table using our api
    /// </summary>
    /// <returns></returns>
    public async Task<Genre[]> GetGenresAsync()
        => await httpClient.GetFromJsonAsync<Genre[]>("genres") ?? [];
}
