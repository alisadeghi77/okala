using System.Net.Http.Json;
using CryptoQuotes.Core;
using Microsoft.Extensions.Options;

namespace CryptoQuotes.Infrastructure;

public class ExchangeRatesService(
    HttpClient httpClient,
    IOptions<ExchangeRatesSettings> exchangeRatesSettings)
    : IExchangeRatesService
{
    private const string StaticEndPoint = "latest?access_key={0}&symbols={1}&base={2}";
    private readonly ExchangeRatesSettings _exchangeRatesSettings = exchangeRatesSettings.Value;

    public async Task<Result<ExchangeRatesResponse>> GetExchangeRatesAsync(
        string baseCurrency,
        IEnumerable<string> symbols,
        CancellationToken cancellationToken)
    {
        try
        {
            var endPoint = string.Format(StaticEndPoint, _exchangeRatesSettings.ApiKey, string.Join(',', symbols), baseCurrency);

            var response = await httpClient.GetAsync(endPoint, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return Result<ExchangeRatesResponse>.Failure(
                    new Error(response.StatusCode.ToString(), $"status code: {response.StatusCode}")
                        .AddData(await response.Content.ReadAsStringAsync(cancellationToken)));

            var result = await response.Content.ReadFromJsonAsync<ExchangeRatesResponse>(cancellationToken);

            if (result is null)
            {
                var exception = new Exception("cryptocurrency/quotes/latest response is null");
                exception.Data.Add("response", await response.Content.ReadAsStringAsync(cancellationToken));
                throw exception;
            }

            return Result<ExchangeRatesResponse>.Success(result);
        }
        catch (HttpRequestException ex)
        {
            return Result<ExchangeRatesResponse>.Failure(new Error("HTTP_ERROR", ex.Message).AddData(ex));
        }
    }
}