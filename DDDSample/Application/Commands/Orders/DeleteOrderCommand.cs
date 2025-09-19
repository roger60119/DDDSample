using MediatR;

namespace DDDSample.Application.Commands.Orders;

public record DeleteOrderCommand(long Id) : IRequest<bool>;