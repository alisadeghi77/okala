using System.Text.Json.Serialization;

namespace CryptoQuotes.Core;

public interface ICoinMarketCapService
{
    Task<Result<CoinMarketCapResponse>> GetCryptoLatestQuotesAsync(
        string cryptoCode,
        string baseCurrency,
        CancellationToken cancellationToken);
}