using Microsoft.Extensions.DependencyInjection;

namespace Flights.Providers.OneSearchProvider.Registers;

public static class OneSearchProviderRegister
{
    public static IServiceCollection AddOneSearchProvider(this IServiceCollection services)
    {
        services.AddScoped<IOneSearchProvider, OneSearchProvider>();

        return services;
    }
}