using Flights.Providers.OneSearchProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Providers.MongoCacheProvider.Registers;

public static class MongoCacheProviderRegister
{
    public static IServiceCollection AddMongoCacheProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IOneSearchProvider, OneSearchProvider.OneSearchProvider>();
        return serviceCollection;
    }
}