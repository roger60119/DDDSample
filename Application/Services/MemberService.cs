using DDDSample.Domain.Entities;
using DDDSample.Domain.Repositories;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Services;

public class MemberService
{
    private readonly IMemberRepository _repository;

    public MemberService(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MemberDto>> GetAllAsync()
    {
        var members = await _repository.GetAllAsync();
        return members.Select(m => new MemberDto { Id = m.Id, Name = m.Name, Mail = m.Mail });
    }

    public async Task<MemberDto?> GetByIdAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id);
        if (member == null) return null;
        return new MemberDto { Id = member.Id, Name = member.Name, Mail = member.Mail };
    }

    public async Task<MemberDto> AddAsync(MemberDto dto)
    {
        var member = new Member(dto.Name, dto.Mail);
        await _repository.AddAsync(member);
        dto.Id = member.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, MemberDto dto)
    {
        var member = await _repository.GetByIdAsync(id);
        if (member == null) return false;
        member.Update(dto.Name, dto.Mail);
        await _repository.UpdateAsync(member);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id);
        if (member == null) return false;
        await _repository.DeleteAsync(member);
        return true;
    }
}