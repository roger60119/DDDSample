using MediatR;
using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Members.Entities;
using DDDSample.Domain.Members.Repositories;

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