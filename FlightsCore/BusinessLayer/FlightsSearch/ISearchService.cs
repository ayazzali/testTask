using Flights.BusinessLayer.FlightsSearch.Models;

namespace Flights.BusinessLayer.FlightsSearch;

public interface ISearchService
{
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
}