using MediatR;

namespace DDDSample.Application.Commands.Members;

public record DeleteMemberCommand(int Id) : IRequest<bool>;