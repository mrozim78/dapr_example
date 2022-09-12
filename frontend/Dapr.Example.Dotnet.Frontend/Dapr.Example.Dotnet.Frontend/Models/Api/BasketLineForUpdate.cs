using System.ComponentModel.DataAnnotations;

namespace Dapr.Example.Dotnet.Frontend.Models.Api;

public class BasketLineForUpdate
{
    [Required]
    public Guid LineId { get; set; }
    [Required]
    public int TicketAmount { get; set; }
}