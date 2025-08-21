using MediatR;
using DDDSample.Domain.Repositories;
using AutoMapper;
using DDDSample.Application.DTOs;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id);
        if (order == null) return false;
        _mapper.Map(request.Dto, order);
        await _repository.UpdateAsync(order);
        return true;
    }
}