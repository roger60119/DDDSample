using DDDSample.Domain.Products.Entities;
using DDDSample.Domain.Products.Repositories;
using DDDSample.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MyDbContext _context;
    private readonly IRedisCacheService _cacheService;

    public ProductRepository(MyDbContext context, IRedisCacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        // Check cache first
        if (_cacheService.Exists("products"))
        {
            return await _cacheService.GetAsync<IEnumerable<Product>>("products");
        }
        // If not in cache, fetch from database
        var products = await _context.Products.AsNoTracking().ToListAsync();
        // Store in cache
        _cacheService.SetAsync("products", products, TimeSpan.FromMinutes(15));
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        if (_cacheService.Exists("products"))
        {
            var products = await _cacheService.GetAsync<IEnumerable<Product>>("products");
            return products.FirstOrDefault(p => p.Id == id);
        }
        return await _context.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("products");
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("products");
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("products");
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Products.AnyAsync(e => e.Id == id);
}