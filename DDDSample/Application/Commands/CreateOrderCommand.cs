using MediatR;
using DDDSample.Application.DTOs;

public record CreateOrderCommand(OrderDto Dto) : IRequest<OrderDto>;