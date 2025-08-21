using MediatR;
using DDDSample.Application.DTOs;

public record UpdateOrderCommand(int Id, OrderDto Dto) : IRequest<bool>;