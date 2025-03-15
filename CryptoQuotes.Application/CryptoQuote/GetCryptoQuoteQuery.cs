using CryptoQuotes.Core;
using MediatR;
using Microsoft.Extensions.Options;

namespace CryptoQuotes.Application;

public record GetCryptoQuoteQuery(string CryptoCode) : IRequest<Result<CryptoQuoteDto>>;

public class GetCryptoQuoteQueryHandler(
    ICoinMarketCapService coinMarketCapService,
    IExchangeRatesService exchangeRatesService,
    IOptions<CurrencySettings> currencySettings)
    : IRequestHandler<GetCryptoQuoteQuery, Result<CryptoQuoteDto>>
{
    private readonly CurrencySettings _currencySettings = currencySettings.Value;

    public async Task<Result<CryptoQuoteDto>> Handle(GetCryptoQuoteQuery request, CancellationToken cancellationToken)
    {
        var normalizeInputCrypto = request.CryptoCode.ToUpper();
        
        var exchangeRatesTask = exchangeRatesService.GetExchangeRatesAsync(
            _currencySettings.BaseCurrency,
            _currencySettings.CurrencySymbols,
            cancellationToken);

        var cryptoLatestTask = coinMarketCapService
            .GetCryptoLatestQuotesAsync(normalizeInputCrypto, _currencySettings.BaseCurrency, cancellationToken);

        await Task.WhenAll(cryptoLatestTask, exchangeRatesTask);

        var coinResult = cryptoLatestTask.Result;
        var exchangeResult = exchangeRatesTask.Result;

        if (!coinResult.IsSuccess || !string.IsNullOrEmpty(coinResult.Value?.Status?.ErrorMessage))
            return Result<CryptoQuoteDto>.Failure(coinResult.Error);

        if (!exchangeResult.IsSuccess)
            return Result<CryptoQuoteDto>.Failure(exchangeResult.Error);

        var coinUsd = coinResult?.Value?.Data[normalizeInputCrypto].FirstOrDefault()?.Quote.FirstOrDefault().Value.Price;

        if (!coinUsd.HasValue)
            return Result<CryptoQuoteDto>
                .Failure(new Error("CoinMarketCapServiceError", "base price not found").AddData(coinResult));

        return Result<CryptoQuoteDto>.Success(new(
            normalizeInputCrypto,
            _currencySettings.CurrencySymbols.ToDictionary(currency => currency,
                s => coinUsd.Value * exchangeResult.Value.Rates[s])));
    }
}