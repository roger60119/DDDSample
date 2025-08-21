using MediatR;

public record DeleteMemberCommand(int Id) : IRequest<bool>;