namespace Flights.Providers.MongoCacheProvider.Configs;

public class RouteDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string RouteCollectionName { get; set; }
}