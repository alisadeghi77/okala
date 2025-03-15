namespace CryptoQuotes.Core;

public record CoinMarketCapSettings{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
}