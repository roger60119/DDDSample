using MediatR;
using DDDSample.Domain.Repositories;
using AutoMapper;
using DDDSample.Application.DTOs;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetAllOrdersQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}