using GloboTicket.Client.Models.Api;

namespace GloboTicket.Client.Models.View;

public class EventListViewModel
{
    public IEnumerable<Event>? Events { get; init; }
    public Guid SelectedCategory { get; init; }
    public IEnumerable<Category>? Categories { get; init; }
}