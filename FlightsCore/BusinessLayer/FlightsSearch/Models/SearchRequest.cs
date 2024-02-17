using System.Text.Json;

namespace Flights.BusinessLayer.FlightsSearch.Models;

public class SearchRequest
{
    // Mandatory
    // Start point of route, e.g. Moscow 
    public string Origin { get; set; }

    // Mandatory
    // End point of route, e.g. Sochi
    public string Destination { get; set; }

    // Mandatory
    // Start date of route
    public DateTime OriginDateTime { get; set; }

    // Optional
    public SearchFilters? Filters { get; set; }


    public SearchRequest Clone()
    {
        if (this == default) throw new Exception("Cannot clone object. SearchRequest is null");

        var me = JsonSerializer.Serialize(this);
        return JsonSerializer.Deserialize<SearchRequest>(me) ?? throw new Exception("Deserialize returned null");
    }
}