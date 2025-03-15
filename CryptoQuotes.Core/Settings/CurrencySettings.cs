namespace CryptoQuotes.Core;

public record CurrencySettings
{
    public string BaseCurrency { get; set; }
    public IEnumerable<string> CurrencySymbols { get; set; }
}