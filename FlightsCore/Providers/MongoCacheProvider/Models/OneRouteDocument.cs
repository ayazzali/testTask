using Flights.Providers.OneSearchProvider.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Flights.Providers.MongoCacheProvider.Models;

public class OneRouteDocument : ProviderOneRoute
{
    [BsonId] public int Id { get; set; }
}