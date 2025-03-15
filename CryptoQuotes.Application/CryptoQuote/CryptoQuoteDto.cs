namespace CryptoQuotes.Core;

public record CryptoQuoteDto(string CryptoCode, Dictionary<string, decimal> Quotes);