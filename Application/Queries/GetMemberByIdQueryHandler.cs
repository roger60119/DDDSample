using MediatR;
using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Members.Repositories;

public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberDto?>
{
    private readonly IMemberRepository _repository;
    private readonly IMapper _mapper;

    public GetMemberByIdQueryHandler(IMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MemberDto?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id);
        return member == null ? null : _mapper.Map<MemberDto>(member);
    }
}