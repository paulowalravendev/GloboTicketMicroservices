using GloboTicket.Grpc;
using Microsoft.AspNetCore.Mvc;
using GloboTicket.Client.Models.View;

namespace GloboTicket.Client.Controllers;

public class EventCatalogController : Controller
{
    private readonly Events.EventsClient _eventCatalogService;

    public EventCatalogController(Events.EventsClient eventCatalogService)
    {
        _eventCatalogService = eventCatalogService;
    }

    public async Task<ActionResult<EventListViewModel>> Index(Guid categoryId)
    {
        var getCategoriesAsync = _eventCatalogService.GetAllCategoriesAsync(new GetAllCategoriesRequest {});
        var getEventsAsync = categoryId == Guid.Empty ?
            _eventCatalogService.GetAllAsync(new GetAllEventsRequest {}) :
            _eventCatalogService.GetAllEventsByCategoryIdAsync(new GetAllEventsByCategoryIdRequest {CategoryId = categoryId.ToString()});
        await Task.WhenAll(getCategoriesAsync.ResponseAsync, getEventsAsync.ResponseAsync);
        return View(new EventListViewModel()
        {
            Events = getEventsAsync.ResponseAsync.Result.Events,
            Categories = getCategoriesAsync.ResponseAsync.Result.Categories,
            SelectedCategory = categoryId
        });
    }

    [HttpPost]
    public IActionResult SelectCategory([FromForm] Guid selectedCategory)
    {
        return RedirectToAction("Index", new {categoryId = selectedCategory});
    }

    public async Task<IActionResult> Detail(Guid eventId)
    {
        var ev = await _eventCatalogService.GetByEventIdAsync(new GetByEventIdRequest {EventId = eventId.ToString()});
        return View(ev.Event);
    }
}