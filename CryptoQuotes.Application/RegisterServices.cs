using Microsoft.Extensions.DependencyInjection;

namespace CryptoQuotes.Application;

public static class RegisterServices
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterServices).Assembly));
        return services;
    }
}