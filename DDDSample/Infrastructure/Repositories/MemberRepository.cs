using AutoMapper.Execution;
using DDDSample.Domain.Members.Entities;
using DDDSample.Domain.Members.Repositories;
using DDDSample.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Member = DDDSample.Domain.Members.Entities.Member;

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
            return await _cacheService.GetAsync<IEnumerable<Member>>("members");
        }
        // If not in cache, fetch from database
        var members = await _context.Members.AsNoTracking().ToListAsync();
        // Store in cache
        _cacheService.SetAsync("members", members, TimeSpan.FromMinutes(15));
        return members;
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        if (_cacheService.Exists("members"))
        {
            var members = await _cacheService.GetAsync<IEnumerable<Member>>("members");
            return members.FirstOrDefault(m => m.MemberId == id);
        }
        return await _context.Members.FindAsync(id);
    }
        

    public async Task AddAsync(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("members");
    }

    public async Task UpdateAsync(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("members");
    }

    public async Task DeleteAsync(Member member)
    {
        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        _cacheService.RemoveAsync("members");
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Members.AnyAsync(e => e.MemberId == id);
}