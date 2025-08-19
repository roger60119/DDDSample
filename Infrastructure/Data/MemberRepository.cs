using DDDSample.Domain.Entities;
using DDDSample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Infrastructure.Data;

public class MemberRepository : IMemberRepository
{
    private readonly MyDbContext _context;

    public MemberRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
        => await _context.Members.AsNoTracking().ToListAsync();

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