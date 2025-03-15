using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public interface IExchangeRatesService
{
    Task<Result<ExchangeRatesResponse>> GetExchangeRatesAsync(
        string baseCurrency,
        IEnumerable<string> symbols,
        CancellationToken cancellationToken);
}