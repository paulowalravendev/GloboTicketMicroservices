using GloboTicket.Client.Models.Api;

namespace GloboTicket.Client.Services;

public interface IEventCatalogService
{
    Task<IEnumerable<Event>> GetAll();
    Task<IEnumerable<Event>> GetByCategoryId(Guid categoryId);
    Task<Event> GetEvent(Guid id);
    Task<IEnumerable<Category>> GetCategories();
}