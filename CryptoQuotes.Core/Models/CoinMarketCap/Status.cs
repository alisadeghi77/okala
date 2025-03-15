using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public record Status(
    [property: JsonPropertyName("timestamp")]
    DateTime Timestamp,
    [property: JsonPropertyName("error_code")]
    int? ErrorCode,
    [property: JsonPropertyName("error_message")]
    string? ErrorMessage,
    [property: JsonPropertyName("elapsed")]
    int? Elapsed,
    [property: JsonPropertyName("credit_count")]
    int? CreditCount,
    [property: JsonPropertyName("notice")] string? Notice
);