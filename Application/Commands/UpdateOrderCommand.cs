using MediatR;
using DDDSample.Application.DTOs;

public record UpdateOrderCommand(long Id, OrderDto Dto) : IRequest<bool>;