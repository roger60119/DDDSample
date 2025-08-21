using MediatR;
using DDDSample.Domain.Repositories;
using AutoMapper;
using DDDSample.Domain.Entities;
using DDDSample.Application.DTOs;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request.Dto);
        await _repository.AddAsync(order);
        return _mapper.Map<OrderDto>(order);
    }
}