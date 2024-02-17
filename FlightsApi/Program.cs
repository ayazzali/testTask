using Flights.BusinessLayer.FlightsSearch;
using Flights.BusinessLayer.FlightsSearch.Models;
using Flights.BusinessLayer.FlightsSearch.Registers;
using Flights.Providers.MongoCacheProvider;
using Flights.Providers.MongoCacheProvider.Configs;
using Flights.Providers.MongoCacheProvider.Registers;
using Flights.Providers.OneSearchProvider;
using Flights.Providers.OneSearchProvider.Registers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFlightsSearchProvider();
builder.Services.AddMongoCacheProvider();
builder.Services.AddOneSearchProvider();
builder.Services.Configure<RouteDbSettings>(builder.Configuration.GetSection("RouteDatabase"));
builder.Services.AddSingleton<IMongoCacheService, MongoCacheService>();
builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/flights/search", async ([FromBody] SearchRequest request, [FromServices] ISearchService searchService,
        CancellationToken cancellationToken) =>
    {
        return await searchService.SearchAsync(request, cancellationToken);
    })
    .WithName("FlightsSearch")
    .WithOpenApi();

app.MapGet("/flights/ping", async ([FromServices] ISearchService searchService,
        CancellationToken cancellationToken) =>
    {
        return await searchService.IsAvailableAsync(cancellationToken);
    })
    .WithName("FlightsPing")
    .WithOpenApi();

app.Run();