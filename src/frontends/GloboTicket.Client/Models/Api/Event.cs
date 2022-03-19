namespace GloboTicket.Client.Models.Api;

public class Event
{
    public Guid EventId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public int Price { get; set; }
    public DateTimeOffset Date { get; set; }
}