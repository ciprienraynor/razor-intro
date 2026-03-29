using System.Text.Json.Serialization;

namespace _10.StateManagement.Effects.Data.ChainingSync;

public sealed record OrderDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("userId")] int UserId,
    [property: JsonPropertyName("total")] decimal Total);