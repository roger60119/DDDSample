using DDDSample.Domain.Members.Entities;

namespace DDDSample.Domain.Members.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(long id);
    Task AddAsync(Order order);
    Task DeleteAsync(Order order);
    Task<bool> ExistsAsync(long id);
}