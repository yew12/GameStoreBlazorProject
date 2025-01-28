namespace GameStore.Api.Contracts;

/// <summary>
/// Records are immutable!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
/// AKA cannot be changed yo
/// </summary>
public record class GameSummaryDto(int Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);
