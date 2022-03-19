using GloboTicket.Client.Extensions;
using GloboTicket.Client.Models.Api;

namespace GloboTicket.Client.Services;

public class EventCatalogService : IEventCatalogService
{
    private readonly HttpClient _httpClient;

    public EventCatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Event>> GetAll()
    {
        var response = await _httpClient.GetAsync("/api/events");
        return await response.ReadContentAs<List<Event>>();
    }

    public async Task<IEnumerable<Event>> GetByCategoryId(Guid categoryId)
    {
        var response = await _httpClient.GetAsync($"/api/events/?categoryId={categoryId}");
        return await response.ReadContentAs<List<Event>>();
    }

    public async Task<Event> GetEvent(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/events/{id}");
        return await response.ReadContentAs<Event>();
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        var response = await _httpClient.GetAsync("/api/categories");
        return await response.ReadContentAs<List<Category>>();
    }

}