namespace GameStore.Api.Contracts;

/// <summary>
/// Records are immutable!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
/// AKA cannot be changed yo
/// </summary>
public record class GameDetailsDto(int Id, string Name, int GenreId, decimal Price, DateOnly ReleaseDate);
