using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public record Platform(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("token_address")] string TokenAddress
);