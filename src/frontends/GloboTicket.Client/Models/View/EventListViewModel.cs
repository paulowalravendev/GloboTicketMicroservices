using GloboTicket.Client.Models.Api;

namespace GloboTicket.Client.Models.View;

public class EventListViewModel
{
    public IEnumerable<Event>? Events { get; set; }
    public Guid SelectedCategory { get; set; }
    public IEnumerable<Category>? Categories { get; set; }
}