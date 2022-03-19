using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GloboTicket.Client.Models;
using GloboTicket.Client.Models.View;
using GloboTicket.Client.Services;

namespace GloboTicket.Client.Controllers;

public class EventCatalogController : Controller
{
    private readonly IEventCatalogService _eventCatalogService;
    private readonly Settings _settings;


    public EventCatalogController(IEventCatalogService eventCatalogService, Settings settings)
    {
        _eventCatalogService = eventCatalogService;
        _settings = settings;
    }

    public async Task<ActionResult<EventListViewModel>> Index(Guid categoryId)
    {
        var getCategoriesTask = _eventCatalogService.GetCategories();
        var getEventsTask = categoryId == Guid.Empty ? _eventCatalogService.GetAll() :
            _eventCatalogService.GetByCategoryId(categoryId);
        await Task.WhenAll(new Task[] {getCategoriesTask, getEventsTask});

        return View(new EventListViewModel()
        {
            Events = getEventsTask.Result,
            Categories = getCategoriesTask.Result,
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
        var ev = await _eventCatalogService.GetEvent(eventId);
        return View(ev);
    }
}