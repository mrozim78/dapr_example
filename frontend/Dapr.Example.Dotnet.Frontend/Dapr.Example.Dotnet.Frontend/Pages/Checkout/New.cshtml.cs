using Dapr.Example.Dotnet.Frontend.Extensions;
using Dapr.Example.Dotnet.Frontend.Models;
using Dapr.Example.Dotnet.Frontend.Models.View;
using Dapr.Example.Dotnet.Frontend.Services.Ordering;
using Dapr.Example.Dotnet.Frontend.Services.ShoppingBasket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dapr.Example.Dotnet.Frontend.Pages.Checkout;

public class New : PageModel
{
    [BindProperty]
    public CheckoutViewModel Checkout { get; set; }

    private IOrderSubmissionService _orderSubmissionService;
    private IShoppingBasketService _shoppingBasketService;
    private ILogger<New> _logger;
    private Settings _settings;
    public New(IOrderSubmissionService orderSubmissionService, IShoppingBasketService shoppingBasketService,
        ILogger<New> logger,Settings settings)
    {
        _orderSubmissionService = orderSubmissionService;
        _shoppingBasketService = shoppingBasketService;
        _logger = logger;
        _settings = settings;
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var currentBasketId = Request.Cookies.GetCurrentBasketId(_settings);
            Checkout.BasketId = currentBasketId;

            _logger.LogInformation($"Received an order from {Checkout.Name}");
            var orderId = await _orderSubmissionService.SubmitOrder(Checkout);
            await _shoppingBasketService.ClearBasket(currentBasketId);
            return RedirectToPage("/Checkout/Thanks");
        }

        return Page();
    }
}