using DDDSample.Domain.Entities;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Repositories;

namespace DDDSample.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await _repository.GetAllAsync();
        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            OrderDate = o.OrderDate,
            MemberId = o.MemberId
        });
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return null;
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            MemberId = order.MemberId
        };
    }

    public async Task<OrderDto> AddAsync(OrderDto dto)
    {
        var order = new Order(dto.OrderNumber, dto.OrderDate, dto.MemberId);
        await _repository.AddAsync(order);
        dto.Id = order.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, OrderDto dto)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return false;
        order.Update(dto.OrderNumber, dto.OrderDate, dto.MemberId);
        await _repository.UpdateAsync(order);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) return false;
        await _repository.DeleteAsync(order);
        return true;
    }
}