using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public record ExchangeRatesResponse(
    [property: JsonPropertyName("success")]
    bool Success,
    [property: JsonPropertyName("timestamp")]
    long Timestamp,
    [property: JsonPropertyName("base")] string BaseCurrency,
    [property: JsonPropertyName("date")] DateTime Date,
    [property: JsonPropertyName("rates")] Dictionary<string, decimal> Rates
);