using DDDSample.Domain.Members.Entities;
using DDDSample.Domain.Members.Repositories;
using DDDSample.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly MyDbContext _context;
    private readonly IRedisCacheService _cacheService;

    public MemberRepository(MyDbContext context, IRedisCacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        // Check cache first
        if (_cacheService.Exists("members"))
        {
            return _cacheService.Get<IEnumerable<Member>>("members");
        }
        // If not in cache, fetch from database
        var members = await _context.Members.AsNoTracking().ToListAsync();
        // Store in cache
        _cacheService.Set("members", members);
        return members;
    }

    public async Task<Member?> GetByIdAsync(int id)
        => await _context.Members.FindAsync(id);

    public async Task AddAsync(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Member member)
    {
        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Members.AnyAsync(e => e.Id == id);
}