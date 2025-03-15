namespace CryptoQuotes.Core;

public record CryptoQuote(string CryptoCode, Dictionary<string, decimal> Quotes);