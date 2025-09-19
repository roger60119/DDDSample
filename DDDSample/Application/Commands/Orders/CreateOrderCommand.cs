using MediatR;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Commands.Orders;

public record CreateOrderCommand(OrderDto Dto) : IRequest<OrderDto>;