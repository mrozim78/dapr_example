using Dapr.Example.Dotnet.Frontend.Models.Api;

namespace Dapr.Example.Dotnet.Frontend.Services.ShoppingBasket;

public class StateStoreBasket
{
    public Guid BasketId { get; set; }
    public List<BasketLine> Lines { get; set; } = new List<BasketLine>();
    public Guid UserId { get; set; }
}