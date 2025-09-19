using DDDSample.Domain.Orders.Entities;

namespace DDDSample.Domain.Orders.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(long id);
    Task AddAsync(Order order);
    Task DeleteAsync(Order order);
    Task<bool> ExistsAsync(long id);
}