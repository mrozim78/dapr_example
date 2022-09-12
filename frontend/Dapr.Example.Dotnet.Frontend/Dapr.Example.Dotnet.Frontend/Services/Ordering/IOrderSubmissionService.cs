using Dapr.Example.Dotnet.Frontend.Models.View;

namespace Dapr.Example.Dotnet.Frontend.Services.Ordering;

public interface IOrderSubmissionService
{
    Task<Guid> SubmitOrder(CheckoutViewModel checkoutViewModel);
}