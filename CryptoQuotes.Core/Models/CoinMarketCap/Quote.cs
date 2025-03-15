using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public record Quote(
    [property: JsonPropertyName("price")] decimal? Price,
    [property: JsonPropertyName("volume_24h")]
    decimal? Volume24H,
    [property: JsonPropertyName("volume_change_24h")]
    decimal? VolumeChange24H,
    [property: JsonPropertyName("percent_change_1h")]
    decimal? PercentChange1H,
    [property: JsonPropertyName("percent_change_24h")]
    decimal? PercentChange24H,
    [property: JsonPropertyName("percent_change_7d")]
    decimal? PercentChange7d,
    [property: JsonPropertyName("percent_change_30d")]
    decimal? PercentChange30d,
    [property: JsonPropertyName("percent_change_60d")]
    decimal? PercentChange60d,
    [property: JsonPropertyName("percent_change_90d")]
    decimal? PercentChange90d,
    [property: JsonPropertyName("market_cap")]
    decimal? MarketCap,
    [property: JsonPropertyName("market_cap_dominance")]
    decimal? MarketCapDominance,
    [property: JsonPropertyName("fully_diluted_market_cap")]
    decimal? FullyDilutedMarketCap,
    [property: JsonPropertyName("tvl")] decimal? Tvl,
    [property: JsonPropertyName("last_updated")]
    DateTime LastUpdated
);