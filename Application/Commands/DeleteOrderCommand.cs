using MediatR;

public record DeleteOrderCommand(long Id) : IRequest<bool>;