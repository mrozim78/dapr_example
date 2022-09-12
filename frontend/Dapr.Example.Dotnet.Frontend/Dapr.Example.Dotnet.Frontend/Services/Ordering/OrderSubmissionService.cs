using System.Text.Json;
using Dapr.Client;
using Dapr.Example.Dotnet.Frontend.Models.Api;
using Dapr.Example.Dotnet.Frontend.Models.View;
using Dapr.Example.Dotnet.Frontend.Services.ShoppingBasket;

namespace Dapr.Example.Dotnet.Frontend.Services.Ordering;

public class OrderSubmissionService:IOrderSubmissionService
{
    private IShoppingBasketService _shoppingBasketService;
    private DaprClient _daprClient;
    private ILogger<OrderSubmissionService> _logger;
    public OrderSubmissionService(IShoppingBasketService shoppingBasketService, DaprClient daprClient,
        ILogger<OrderSubmissionService> logger)
    {
        _shoppingBasketService = shoppingBasketService;
        _daprClient = daprClient;
        _logger = logger;
    }
        
    public async Task<Guid> SubmitOrder(CheckoutViewModel checkoutViewModel)
    {
        var lines = await _shoppingBasketService.GetLinesForBasket(checkoutViewModel.BasketId);
        var order = new OrderForCreation();
        order.Date = DateTimeOffset.Now;
        order.OrderId = Guid.NewGuid();
        order.Lines = lines.Select(line => new OrderLine() { 
            EventId = line.EventId, Price = line.Price, 
            TicketCount = line.TicketAmount }).ToList();
        order.CustomerDetails = new CustomerDetails()
        {
            Address = checkoutViewModel.Address,
            CreditCardNumber = checkoutViewModel.CreditCard,
            Email = checkoutViewModel.Email,
            Name = checkoutViewModel.Name,
            PostalCode = checkoutViewModel.PostalCode,
            Town = checkoutViewModel.Town,
            CreditCardExpiryDate = checkoutViewModel.CreditCardDate
        };
        _logger.LogInformation("Posting order event to Dapr pubsub");
        var options = new JsonSerializerOptions { WriteIndented = true };
        _logger.LogInformation(JsonSerializer.Serialize(order,options));
        await _daprClient.PublishEventAsync("pubsub", "orders", order);
        return order.OrderId;
    }
}