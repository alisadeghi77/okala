namespace CryptoQuotes.Core;

public record ExchangeRatesSettings
{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
}