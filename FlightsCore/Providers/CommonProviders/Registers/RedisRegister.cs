using Microsoft.Extensions.DependencyInjection;

namespace Flights.Providers.CommonProviders.Registers;

public static class RedisRegister
{
    public static IServiceCollection AddRedis(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddStackExchangeRedisCache(opts =>
        {
            opts.Configuration = "localhost";
            opts.InstanceName = "local";
        });
        return serviceCollection;
    }
}