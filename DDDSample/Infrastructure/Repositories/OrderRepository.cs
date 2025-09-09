using DDDSample.Domain.Members.Entities;
using DDDSample.Domain.Members.Repositories;
using DDDSample.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace DDDSample.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly MyDbContext _context;
    private readonly IRedisCacheService _cacheService;

    public OrderRepository(MyDbContext context, IRedisCacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        // Check cache first
        if (_cacheService.Exists("orders"))
        {
            return await _cacheService.GetAsync<IEnumerable<Order>>("orders");
        }
        // If not in cache, fetch from database
        var orders = await _context.Orders.AsNoTracking().ToListAsync();
        // Store in cache
        _cacheService.SetAsync("orders", orders, TimeSpan.FromMinutes(15));
        return orders;
    }

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