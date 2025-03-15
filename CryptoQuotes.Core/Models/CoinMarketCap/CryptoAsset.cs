using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public record CryptoAsset(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("num_market_pairs")] int? NumMarketPairs,
    [property: JsonPropertyName("date_added")] DateTime DateAdded,
    [property: JsonPropertyName("tags")] List<Tag> Tags,
    [property: JsonPropertyName("max_supply")] long? MaxSupply,
    [property: JsonPropertyName("circulating_supply")] decimal? CirculatingSupply,
    [property: JsonPropertyName("total_supply")] decimal? TotalSupply,
    [property: JsonPropertyName("platform")] Platform? Platform,
    [property: JsonPropertyName("is_active")] int? IsActive,
    [property: JsonPropertyName("infinite_supply")] bool InfiniteSupply,
    [property: JsonPropertyName("cmc_rank")] int? CmcRank,
    [property: JsonPropertyName("is_fiat")] int? IsFiat,
    [property: JsonPropertyName("self_reported_circulating_supply")] decimal? SelfReportedCirculatingSupply,
    [property: JsonPropertyName("self_reported_market_cap")] decimal? SelfReportedMarketCap,
    [property: JsonPropertyName("quote")] Dictionary<string, Quote> Quote
);