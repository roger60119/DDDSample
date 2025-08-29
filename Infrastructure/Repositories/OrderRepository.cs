using DDDSample.Domain.Members.Entities;
using DDDSample.Domain.Members.Repositories;
using DDDSample.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly MyDbContext _context;

    public OrderRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
        => await _context.Orders.AsNoTracking().ToListAsync();

    public async Task<Order?> GetByIdAsync(long id)
        => await _context.Orders.FindAsync(id);

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(long id)
        => await _context.Orders.AnyAsync(e => e.OrderId == id);
}