using AutoMapper;
using DDDSample.Domain.Entities;
using DDDSample.Domain.Repositories;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetByIdAsync(long id)
    {
        var order = await _repository.GetByIdAsync(id);
        return order == null ? null : _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> AddAsync(OrderDto dto)
    {
        var order = _mapper.Map<Order>(dto);
        await _repository.AddAsync(order);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<bool> UpdateAsync(long id, OrderDto dto)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return false;
        _mapper.Map(dto, order);
        await _repository.UpdateAsync(order);
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return false;
        await _repository.DeleteAsync(order);
        return true;
    }
}