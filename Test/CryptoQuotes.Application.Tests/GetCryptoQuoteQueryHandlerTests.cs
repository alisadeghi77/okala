using CryptoQuotes.Application;
using CryptoQuotes.Core;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Options;

public class GetCryptoQuoteQueryHandlerTests
{
    private readonly Mock<ICoinMarketCapService> _coinMarketCapServiceMock;
    private readonly Mock<IExchangeRatesService> _exchangeRatesServiceMock;
    private readonly IOptions<CurrencySettings> _currencySettings;
    private readonly GetCryptoQuoteQueryHandler _handler;

    public GetCryptoQuoteQueryHandlerTests()
    {
        _coinMarketCapServiceMock = new Mock<ICoinMarketCapService>();
        _exchangeRatesServiceMock = new Mock<IExchangeRatesService>();

        _currencySettings = Options.Create(new CurrencySettings
        {
            BaseCurrency = "USD",
            CurrencySymbols = new List<string> { "EUR", "GBP" }
        });

        _handler = new GetCryptoQuoteQueryHandler(
            _coinMarketCapServiceMock.Object,
            _exchangeRatesServiceMock.Object,
            _currencySettings);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCoinMarketCapServiceFails()
    {
        // Arrange
        var query = new GetCryptoQuoteQuery("BTC");

        _coinMarketCapServiceMock
            .Setup(s => s.GetCryptoLatestQuotesAsync("BTC", "USD", It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CoinMarketCapResponse>.Failure(new Error("API_ERROR", "Failed to fetch data")));

        _exchangeRatesServiceMock
            .Setup(s => s.GetExchangeRatesAsync("USD", It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<ExchangeRatesResponse>.Success(It.IsAny<ExchangeRatesResponse>()));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("API_ERROR");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExchangeRatesServiceFails()
    {
        // Arrange
        var query = new GetCryptoQuoteQuery("BTC");

        _coinMarketCapServiceMock
            .Setup(s => s.GetCryptoLatestQuotesAsync("BTC", "USD", It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CoinMarketCapResponse>.Success(
                new CoinMarketCapResponse(It.IsAny<Status>(), It.IsAny<Dictionary<string, List<CryptoAsset>>>())));

        _exchangeRatesServiceMock
            .Setup(s => s.GetExchangeRatesAsync("USD", It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                Result<ExchangeRatesResponse>.Failure(new Error("EXCHANGE_ERROR", "Failed to fetch exchange rates")));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("EXCHANGE_ERROR");
    }


    [Theory]
    [InlineData("BTC", 50000.0, 0.85, 0.75)]
    [InlineData("ETH", 3500.0, 0.85, 0.75)]
    [InlineData("LTC", 200.0, 0.85, 0.75)]
    public async Task Handle_ShouldReturnSuccess_WhenServicesReturnValidData(
        string cryptoCode, decimal cryptoPrice, decimal eurRate, decimal gbpRate)
    {
        // Arrange
        var query = new GetCryptoQuoteQuery(cryptoCode);

        _coinMarketCapServiceMock
            .Setup(s => s.GetCryptoLatestQuotesAsync(cryptoCode, "USD", It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<CoinMarketCapResponse>.Success(
                new CoinMarketCapResponse(
                    new Status(DateTime.Now, null, null, null, null, null),
                    new Dictionary<string, List<CryptoAsset>>
                    {
                        {
                            cryptoCode, new List<CryptoAsset>
                            {
                                new(1, "Crypto", cryptoCode, "slug", null, DateTime.Now, null, null, null, null, null,
                                    1,
                                    false, 1, 0, null, null,
                                    new Dictionary<string, Quote>
                                    {
                                        {
                                            "USD", new Quote(cryptoPrice, 1000000.0m, 5.0m, -1.2m, 2.3m, 3.1m, 10.5m,
                                                20.2m, 30.7m, 1000000000.0m, 45.0m, 1200000000.0m, 500000000.0m,
                                                DateTime.Now
                                            )
                                        }
                                    }
                                )
                            }
                        }
                    }
                )
            ));

        _exchangeRatesServiceMock
            .Setup(s => s.GetExchangeRatesAsync("USD", It.IsAny<List<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<ExchangeRatesResponse>.Success(
                new ExchangeRatesResponse(
                    true,
                    It.IsAny<long>(),
                    "USD",
                    It.IsAny<DateTime>(),
                    new Dictionary<string, decimal>
                    {
                        { "EUR", eurRate },
                        { "GBP", gbpRate }
                    })));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.CryptoCode.Should().Be(cryptoCode);
        result.Value.Quotes["EUR"].Should().Be(cryptoPrice * eurRate);
        result.Value.Quotes["GBP"].Should().Be(cryptoPrice * gbpRate);
    }
}