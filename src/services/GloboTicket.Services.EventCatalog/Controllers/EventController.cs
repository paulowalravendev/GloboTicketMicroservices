using AutoMapper;
using GloboTicket.Services.EventCatalog.Models;
using GloboTicket.Services.EventCatalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Services.EventCatalog.Controllers;

[Route("api/events")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public EventController(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> Get(
        [FromQuery] Guid categoryId)
    {
        var result = await _eventRepository.GetEvents(categoryId);
        return Ok(_mapper.Map<List<EventDto>>(result));
    }

    [HttpGet("{eventId}")]
    public async Task<ActionResult<EventDto>> GetById(Guid eventId)
    {
        var result = await _eventRepository.GetEventById(eventId);
        return Ok(_mapper.Map<EventDto>(result));
    }
}