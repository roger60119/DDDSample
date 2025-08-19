using DDDSample.Domain.Entities;

namespace DDDSample.Domain.Repositories;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
    Task DeleteAsync(Member member);
    Task<bool> ExistsAsync(int id);
}