using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GameStore.Frontend.Converters;

namespace GameStore.Frontend.Models;

public class GameDetails
{
    public int Id { get; set; }

    /// <summary>
    /// [Required] -> blazor' way of making sure user enters a value for the Name field
    /// [StringLength(50)] - no more than 50 characters
    /// </summary>
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The Genre Field is required.")]
    [JsonConverter(typeof(Converters.StringConverter))] // uses our string converter that we implemented to transform number we get during deserialization 
    public string? GenreId { get; set; }

    /// <summary>
    /// [Range(1,100)] -> 1 to 100 dollar price
    /// </summary>
    [Range(1, 100)]
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
