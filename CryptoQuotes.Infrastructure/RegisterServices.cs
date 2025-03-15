using CryptoQuotes.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CryptoQuotes.Infrastructure;

public static class RegisterInfrastructureServices
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExchangeRatesSettings>(configuration.GetSection("ExchangeRatesSettings"));
        services.Configure<CoinMarketCapSettings>(configuration.GetSection("CoinMarketCapSettings"));
        
        services.AddHttpClient<ICoinMarketCapService, CoinMarketCapService>((provider, client) =>
        {
            var settings = provider.GetRequiredService<IOptions<CoinMarketCapSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseUrl);
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", settings.ApiKey);
        });
        
        services.AddHttpClient<IExchangeRatesService, ExchangeRatesService>((provider, client) =>
        {
            var settings = provider.GetRequiredService<IOptions<ExchangeRatesSettings>>().Value;
            client.BaseAddress = new Uri(settings.BaseUrl);
        });

        return services;
    }
}