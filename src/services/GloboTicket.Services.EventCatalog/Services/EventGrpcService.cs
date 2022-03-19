using AutoMapper;
using GloboTicker.Grpc;
using GloboTicket.Services.EventCatalog.Repositories;
using Grpc.Core;

namespace GloboTicket.Services.EventCatalog.Services;

public class EventGrpcService : Events.EventsBase
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;
    private readonly ICategoryRepository _categoryRepository;

    public EventGrpcService(IMapper mapper, IEventRepository eventRepository, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
    }

    public override async Task<GetAllEventsResponse> GetAll(GetAllEventsRequest request, ServerCallContext context)
    {
        var response = new GetAllEventsResponse();
        var eventEntities = await _eventRepository.GetEvents(Guid.Empty);
        response.Events.Add(_mapper.Map<List<Event>>(eventEntities));
        return response;
    }

    public override async Task<GetAllEventsResponse> GetAllEventsByCategoryId(GetAllEventsByCategoryIdRequest request, ServerCallContext context)
    {
        var response = new GetAllEventsResponse();
        var eventEntities = await _eventRepository.GetEvents(Guid.Parse(request.CategoryId));
        response.Events.Add(_mapper.Map<List<Event>>(eventEntities));
        return response;
    }
}