using DDDSample.Domain.Orders.Entities;
using DDDSample.Domain.Orders.Repositories;
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
        // If not in cache, fetch from database (include OrderItems and Product)
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .AsNoTracking()
            .ToListAsync();
        // Store in cache
        _cacheService.SetAsync("orders", orders, TimeSpan.FromMinutes(15));
        return orders;
    }

    public async Task<Order?> GetByIdAsync(long id)
        => await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id);

    public async Task AddAsync(Order order)
    {
        // 確保 OrderItems 只關聯 ProductId
        foreach (var item in order.OrderItems)
        {
            item.Product = null!;
        }
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("orders");
    }

    public async Task UpdateAsync(Order order)
    {
        // 先載入原始訂單及其 OrderItems
        var existingOrder = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);
        if (existingOrder == null) return;

        // 更新主資料
        _context.Entry(existingOrder).CurrentValues.SetValues(order);

        // 處理 OrderItems
        // 刪除不存在的項目
        foreach (var item in existingOrder.OrderItems.ToList())
        {
            if (!order.OrderItems.Any(x => x.ProductId == item.ProductId))
                _context.OrderItems.Remove(item);
        }
        // 新增或更新項目
        foreach (var item in order.OrderItems)
        {
            var existingItem = existingOrder.OrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
            }
            else
            {
                existingOrder.OrderItems.Add(item);
            }
        }
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("orders");
    }

    public async Task DeleteAsync(Order order)
    {
        // 先載入 OrderItems
        var existingOrder = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);
        if (existingOrder == null) return;
        _context.OrderItems.RemoveRange(existingOrder.OrderItems);
        _context.Orders.Remove(existingOrder);
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("orders");
    }

    public async Task<bool> ExistsAsync(long id)
        => await _context.Orders.AnyAsync(e => e.OrderId == id);
}