using Dapr.Example.Dotnet.Frontend.Models;

namespace Dapr.Example.Dotnet.Frontend.Extensions;

public static class RequestCookieCollection
{
    public static Guid GetCurrentBasketId(this IRequestCookieCollection cookies, Settings settings)
    {
        Guid.TryParse(cookies[settings.BasketIdCookieName], out Guid basketId);
        return basketId;
    }
}