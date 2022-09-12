using Dapr.Example.Dotnet.Frontend.Extensions;
using Dapr.Example.Dotnet.Frontend.Models;
using Dapr.Example.Dotnet.Frontend.Models.Api;
using Dapr.Example.Dotnet.Frontend.Models.View;
using Dapr.Example.Dotnet.Frontend.Services.ShoppingBasket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dapr.Example.Dotnet.Frontend.Pages.ShoppingBasket;

public class List : PageModel
{
    [BindProperty]
    public BasketLineForUpdate BasketLineUpdate { get; set; }
    
    public IEnumerable<BasketLineViewModel> BasketLineViewModels { get; set; }
    private IShoppingBasketService _basketService;
    
    
    private Settings _settings;
    public List(IShoppingBasketService basketService,Settings settings)
    {
        _basketService = basketService;
        _settings = settings;
    }
    
    
    

    public async Task OnGet()
    {
        var basketLines = await _basketService.GetLinesForBasket(Request.Cookies.GetCurrentBasketId(_settings));
        BasketLineViewModels = basketLines.Select(bl => new BasketLineViewModel
            {
                LineId = bl.BasketLineId,
                EventId = bl.EventId,
                EventName = bl.Event.Name,
                Date = bl.Event.Date,
                Price = bl.Price,
                Quantity = bl.TicketAmount
            }
        );
    }

    public async Task OnPostUpdateLine()
    {
        if (ModelState.IsValid)
        {
            var basketId = Request.Cookies.GetCurrentBasketId(_settings);
            await _basketService.UpdateLine(basketId, BasketLineUpdate);
        }
        OnGet();
    }
    public async  Task OnGetRemoveLine(Guid lineId)
    {
        var basketId = Request.Cookies.GetCurrentBasketId(_settings);
        await _basketService.RemoveLine(basketId, lineId);
        OnGet();
    }
    


}