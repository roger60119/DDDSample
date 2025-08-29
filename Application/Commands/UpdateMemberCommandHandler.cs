using MediatR;
using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Members.Repositories;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IMemberRepository _repository;
    private readonly IMapper _mapper;

    public UpdateMemberCommandHandler(IMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id);
        if (member == null) return false;
        _mapper.Map(request.Dto, member);
        await _repository.UpdateAsync(member);
        return true;
    }
}