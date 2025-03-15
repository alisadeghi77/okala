using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public record Tag(
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("category")]
    string Category
);