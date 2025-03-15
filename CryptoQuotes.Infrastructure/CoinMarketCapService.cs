using System.Net.Http.Json;
using CryptoQuotes.Core;

namespace CryptoQuotes.Infrastructure;

public class CoinMarketCapService(HttpClient httpClient) : ICoinMarketCapService
{
    private const string StaticEndPoint = "v2/cryptocurrency/quotes/latest?symbol={0}&convert={1}";

    public async Task<Result<CoinMarketCapResponse>> GetCryptoLatestQuotesAsync(
        string cryptoCode,
        string baseCurrency,
        CancellationToken cancellationToken)
    {
        try
        {
            var endPoint = string.Format(StaticEndPoint, cryptoCode, baseCurrency);

            var response = await httpClient.GetAsync(endPoint, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return Result<CoinMarketCapResponse>.Failure(
                    new Error(response.StatusCode.ToString(), $"status code: {response.StatusCode}")
                        .AddData(await response.Content.ReadAsStringAsync(cancellationToken)));

            var result = await response.Content.ReadFromJsonAsync<CoinMarketCapResponse>(cancellationToken);
            
            if (result is null)
            {
                var exception = new Exception("cryptocurrency/quotes/latest response is null");
                exception.Data.Add("response", await response.Content.ReadAsStringAsync(cancellationToken));
                throw exception;
            }
            
            return Result<CoinMarketCapResponse>.Success(result);
        }
        catch (HttpRequestException ex)
        {
            return Result<CoinMarketCapResponse>.Failure(new Error("HTTP_ERROR", ex.Message).AddData(ex));
        }
    }
}