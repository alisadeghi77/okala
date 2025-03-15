using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public record CoinMarketCapResponse(
    [property: JsonPropertyName("status")] Status Status,
    [property: JsonPropertyName("data")] Dictionary<string, List<CryptoAsset>> Data
);