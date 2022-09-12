using System.ComponentModel.DataAnnotations;

namespace Dapr.Example.Dotnet.Frontend.Models.Api;

public class BasketLineForCreation
{
    [Required]
    public Guid EventId { get; set; }
    [Required]
    public int TicketAmount { get; set; }
    [Required]
    public int Price { get; set; }
}