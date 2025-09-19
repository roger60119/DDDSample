using MediatR;
using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Members.Entities;
using DDDSample.Domain.Members.Repositories;

namespace DDDSample.Application.Commands.Members;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberDto>
{
    private readonly IMemberRepository _repository;
    private readonly IMapper _mapper;

    public CreateMemberCommandHandler(IMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MemberDto> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = _mapper.Map<Member>(request.Dto);
        await _repository.AddAsync(member);
        return _mapper.Map<MemberDto>(member);
    }
}