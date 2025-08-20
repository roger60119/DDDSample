using AutoMapper;
using DDDSample.Domain.Entities;
using DDDSample.Domain.Repositories;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Services;

public class MemberService
{
    private readonly IMemberRepository _repository;
    private readonly IMapper _mapper;

    public MemberService(IMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MemberDto>> GetAllAsync()
    {
        var members = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<MemberDto>>(members);
    }

    public async Task<MemberDto?> GetByIdAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id);
        return member == null ? null : _mapper.Map<MemberDto>(member);
    }

    public async Task<MemberDto> AddAsync(MemberDto dto)
    {
        var member = _mapper.Map<Member>(dto);
        await _repository.AddAsync(member);
        return _mapper.Map<MemberDto>(member);
    }

    public async Task<bool> UpdateAsync(int id, MemberDto dto)
    {
        var member = await _repository.GetByIdAsync(id);
        if (member == null) return false;
        // 將 dto 的值映射到已存在的 member 實体
        _mapper.Map(dto, member);
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