using MediatR;
using DDDSample.Domain.Repositories;
using AutoMapper;
using DDDSample.Application.DTOs;

public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, IEnumerable<MemberDto>>
{
    private readonly IMemberRepository _repository;
    private readonly IMapper _mapper;

    public GetAllMembersQueryHandler(IMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MemberDto>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<MemberDto>>(members);
    }
}