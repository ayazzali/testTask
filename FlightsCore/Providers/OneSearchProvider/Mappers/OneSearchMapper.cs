using Flights.BusinessLayer.FlightsSearch.Models;
using Flights.Providers.OneSearchProvider.Models;

namespace Flights.Providers.OneSearchProvider.Mappers;

public static class OneSearchMapper
{
    public static ProviderOneSearchRequest MapRequest(SearchRequest request)
    {
        return new ProviderOneSearchRequest()
        {
            To = request.Destination,
            From = request.Origin,
            DateFrom = request.OriginDateTime,
            MaxPrice = request.Filters?.MaxPrice,
            DateTo = request.Filters?.DestinationDateTime
        };
    }
}