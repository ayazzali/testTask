using Microsoft.Extensions.DependencyInjection;

namespace Flights.BusinessLayer.FlightsSearch.Registers;

public static class FlightsProviderRegister
{
    public static IServiceCollection AddFlightsSearchProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISearchService, SearchService>();
        return serviceCollection;
    }
}
// OneSearchProviderRegister.AddRedis(serviceCollection);