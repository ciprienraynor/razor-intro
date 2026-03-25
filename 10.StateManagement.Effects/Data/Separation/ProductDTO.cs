namespace _10.StateManagement.Effects.Data.Separation;

using System.Text.Json.Serialization;

public sealed record ProductDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("price")] decimal Price,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("thumbnail")] string Thumbnail);