using Faker;
using Flights.BusinessLayer.FlightsSearch.Models;
using Flights.Providers.MongoCacheProvider;
using Flights.Providers.OneSearchProvider.Mappers;
using Flights.Providers.OneSearchProvider.Models;

namespace Flights.Providers.OneSearchProvider;

public class OneSearchProvider : IOneSearchProvider
{
    private readonly HttpClient _httpClient;
    private readonly IMongoCacheService _mongoCacheService;

    public OneSearchProvider(HttpClient httpClient,
        IMongoCacheService mongoCacheService)
    {
        _httpClient = httpClient;
        _mongoCacheService = mongoCacheService;
    }

    public async Task<List<Route>?> Search(SearchRequest request, CancellationToken cancellationToken)
    {
        var oneRequestModel = OneSearchMapper.MapRequest(request);
        if (request.Filters?.OnlyCached == true)
        {
            var resultsFromCache = await _mongoCacheService.FindAsync(oneRequestModel);
            return resultsFromCache.Select(RouteMapper.OneToSearchMap).ToList();
        }

        ProviderOneSearchResponse responseArr = null;
        try
        {
            // var responseString = await _httpClient.PostAsync("localhost/weatherforecast");
            // with retry
            // log
            // var responseArr = JsonSerializer.Deserialize<List<ProviderOneSearchResponse>>(responseString);
            responseArr = ResponseGenerator(oneRequestModel);
        }
        catch
        {
            // log
            var resultsFromCache = await _mongoCacheService.FindAsync(oneRequestModel);
            return resultsFromCache.Select(RouteMapper.OneToSearchMap).ToList();
        }

        var routesForCache = responseArr.Routes.Select(RouteMapper.OneToMongoMap).ToList();
        _ = _mongoCacheService.SetAsync(routesForCache, cancellationToken);

        return responseArr!.Routes.Select(RouteMapper.OneToSearchMap).ToList();
    }

    #region only for developing

    private static readonly Random Rnd = new();

    private ProviderOneSearchResponse ResponseGenerator(ProviderOneSearchRequest request)
    {
        return new ProviderOneSearchResponse
        {
            Routes = RouteGenerator(request).ToArray()
        };
    }

    private IEnumerable<ProviderOneRoute> RouteGenerator(ProviderOneSearchRequest request)
    {
        if (Rnd.Next(5) == 0) throw new Exception("bad connection with provider one}");

        for (var i = 0; i < Rnd.Next(20000); i++)
        {
            var dates = DateGenerator(request.DateFrom, request.DateTo);
            yield return new ProviderOneRoute
            {
                From = request.From,
                To = request.To ?? Address.City(),
                DateFrom = dates.from,
                DateTo = dates.to,
                Price = Rnd.Next(700, (int?)request.MaxPrice ?? 56123),
                TimeLimit = DateTime.UtcNow.AddMinutes(Rnd.Next(5))
            };
        }
    }

    private (DateTime from, DateTime to) DateGenerator(DateTime from, DateTime? to)
    {
        var fromUnix = from.ToFileTimeUtc();
        var toUnix = from.AddHours(Rnd.Next(1, 11)).ToFileTimeUtc();
        var newFromLong = Rnd.NextInt64(fromUnix, toUnix);
        var newFromResult = DateTime.FromFileTimeUtc(newFromLong);
        var newToResult = DateTime.FromFileTimeUtc(Rnd.NextInt64(newFromLong, toUnix));
        return (newFromResult, newToResult);
    }

    #endregion
}

// todo service result

// public async Task<ProviderOneSearchResponse> Search(ProviderTwoSearchRequest request, bool cacheOnly, CancellationToken cancellationToken)
// var requestSerialised = JsonSerializer.Serialize(request, new JsonSerializerOptions() { WriteIndented = false });