using Flights.BusinessLayer.FlightsSearch.Models;
using Flights.Helpers;
using Flights.Providers.MongoCacheProvider.Models;
using Flights.Providers.OneSearchProvider.Models;

namespace Flights.Providers.OneSearchProvider.Mappers;

public static class RouteMapper
{
    public static Route OneToSearchMap(ProviderOneRoute route)
    {
        return new Route()
        {
            Origin = route.From,
            Destination = route.To,
            OriginDateTime = route.DateFrom,
            DestinationDateTime = route.DateTo,
            Price = route.Price,
            TimeLimit = route.TimeLimit,
            Id = GuidHelper.ToGuid(
                (route.From + route.To + route.DateFrom + route.DateTo + route.Price + route.TimeLimit).GetHashCode())
        };
    }

    public static OneRouteDocument OneToMongoMap(ProviderOneRoute route)
    {
        return new OneRouteDocument()
        {
            From = route.From,
            To = route.To,
            DateFrom = route.DateFrom,
            DateTo = route.DateTo,
            Price = route.Price,
            TimeLimit = route.TimeLimit,
            Id = (route.From + route.To + route.DateFrom + route.DateTo + route.Price + route.TimeLimit).GetHashCode()
        };
    }
}