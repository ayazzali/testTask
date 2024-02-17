using Flights.Providers.MongoCacheProvider.Models;
using Flights.Providers.OneSearchProvider.Models;

namespace Flights.Providers.MongoCacheProvider;

public interface IMongoCacheService
{
    Task<List<OneRouteDocument>> FindAsync(ProviderOneSearchRequest request);
    Task SetAsync(IEnumerable<OneRouteDocument> routes, CancellationToken cancellationToken);
    Task RemoveOldAsync(CancellationToken cancellationToken);
}