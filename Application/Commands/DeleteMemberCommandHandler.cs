using MediatR;
using DDDSample.Domain.Members.Repositories;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IMemberRepository _repository;

    public DeleteMemberCommandHandler(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetByIdAsync(request.Id);
        if (member == null) return false;
        await _repository.DeleteAsync(member);
        return true;
    }
}