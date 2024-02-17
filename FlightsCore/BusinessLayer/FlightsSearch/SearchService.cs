using Flights.BusinessLayer.FlightsSearch.Models;
using Flights.Providers.OneSearchProvider;

namespace Flights.BusinessLayer.FlightsSearch;

public class SearchService : ISearchService
{
    private readonly IOneSearchProvider _oneSearchProvider;

    public SearchService(IOneSearchProvider oneSearchProvider)
    {
        _oneSearchProvider = oneSearchProvider;
    }

    public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        // validation of request
        var resultOne = await _oneSearchProvider.Search(request, cancellationToken);
        var resultTwo = new List<Route>(); // _twoSearchProvider.Search(request, cancellationToken);

        if (resultOne == default && resultOne == default)
            // return serviceResult
            throw new NotImplementedException();

        var resultUnion = resultOne.Union(resultTwo).ToArray();
        if (!resultUnion.Any())
            // return serviceResult
            throw new Exception("No routes found");

        var resultUnionMinutes = resultUnion
            .Select(x => (x.DestinationDateTime - x.OriginDateTime).TotalMinutes)
            .ToArray();

        var result = new SearchResponse
        {
            Routes = resultUnion,
            MaxPrice = resultUnion.Max(x => x.Price),
            MinPrice = resultUnion.Min(x => x.Price),
            MaxMinutesRoute = (int)resultUnionMinutes.Max(),
            MinMinutesRoute = (int)resultUnionMinutes.Min()
        };

        return result;
    }

    public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
    {
        // check with business people
        return true;
    }
}