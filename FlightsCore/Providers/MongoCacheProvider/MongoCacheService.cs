using Flights.Providers.MongoCacheProvider.Configs;
using Flights.Providers.MongoCacheProvider.Models;
using Flights.Providers.OneSearchProvider.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Flights.Providers.MongoCacheProvider;

public class MongoCacheService : IMongoCacheService
{
    private readonly IMongoCollection<OneRouteDocument> _routeCollection;

    public MongoCacheService(
        IOptions<RouteDbSettings> routeDatabaseSetting)
    {
        var mongoClient = new MongoClient(
            routeDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            routeDatabaseSetting.Value.DatabaseName);

        _routeCollection = mongoDatabase.GetCollection<OneRouteDocument>(
            routeDatabaseSetting.Value.RouteCollectionName);
    }

    public async Task<List<OneRouteDocument>> FindAsync(ProviderOneSearchRequest request)
    {
        var filter = Builders<OneRouteDocument>.Filter.Gte(x => x.DateFrom, request.DateFrom);
        filter &= Builders<OneRouteDocument>.Filter.Eq(x => x.From, request.From);
        filter &= Builders<OneRouteDocument>.Filter.Eq(x => x.To, request.To);
        filter &= Builders<OneRouteDocument>.Filter.Gt(x => x.TimeLimit, DateTime.UtcNow);

        if (request.DateTo != default)
            filter &= Builders<OneRouteDocument>.Filter.Lte(x => x.DateTo, request.DateTo);

        if (request.MaxPrice != default)
            filter &= Builders<OneRouteDocument>.Filter.Lte(x => x.Price, request.MaxPrice);

        return await _routeCollection.Find(filter, new FindOptions { MaxTime = TimeSpan.FromMilliseconds(200) })
            .ToListAsync();
    }

    public async Task SetAsync(IEnumerable<OneRouteDocument> routes, CancellationToken cancellationToken)
    {
        Console.WriteLine("SetAsync start");
        foreach (var route in routes)
        {
            _ = await _routeCollection.ReplaceOneAsync(x => x.Id == route.Id,
                route,
                new ReplaceOptions
                {
                    IsUpsert = true
                }, cancellationToken);
            Console.WriteLine("ReplaceOneAsync done");
        }

        Console.WriteLine("SetAsync done");
    }

    public async Task RemoveOldAsync(CancellationToken cancellationToken)
    {
        await _routeCollection.DeleteManyAsync(x => x.TimeLimit > DateTime.UtcNow, cancellationToken);
    }
}

// todo _routeCollection.BulkWriteAsync
// todo log exceptions at fire and forget