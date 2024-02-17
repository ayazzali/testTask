using Flights.BusinessLayer.FlightsSearch.Models;

namespace Flights.Providers.OneSearchProvider;

public interface IOneSearchProvider
{
    Task<List<Route>?> Search(SearchRequest request, CancellationToken cancellationToken);
}
// Task<ProviderOneSearchResponse> Search(ProviderTwoSearchRequest request, CancellationToken cancellationToken);