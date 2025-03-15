using CryptoQuotes.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoQuotes.Application;

public static class RegisterServices
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CurrencySettings>(configuration.GetSection("ExchangeRatesSettings"));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterServices).Assembly));
        
        return services;
    }
}