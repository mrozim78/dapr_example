using Dapr.Example.Dotnet.Frontend.Models.Api;

namespace Dapr.Example.Dotnet.Frontend.Services.EventCatalog;

public interface IEventCatalogService 
{
    Task<IEnumerable<Event>> GetAll();

    Task<Event> GetEvent(Guid id);

}