using System.Text.Json;
using Dapr.Example.Dotnet.Frontend.Extensions;
using Dapr.Example.Dotnet.Frontend.Models.Api;

namespace Dapr.Example.Dotnet.Frontend.Services.EventCatalog;

public class EventCatalogService:IEventCatalogService
{
    private readonly HttpClient _client;
    private readonly ILogger<EventCatalogService> _logger;
    public EventCatalogService(HttpClient client , ILogger<EventCatalogService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<IEnumerable<Event>> GetAll()
    {
        var response = await _client.GetAsync("event");

        List<Event> events = await response.ReadContentAs<List<Event>>();
        //_logger.LogInformation(JsonSerializer.Serialize(events));
        return events;
    }

    public async Task<Event> GetEvent(Guid id)
    {
        var response = await _client.GetAsync($"event/{id}");
        return await response.ReadContentAs<Event>();
    }

}