using MediatR;
using DDDSample.Application.DTOs;

public record GetOrderByIdQuery(int Id) : IRequest<OrderDto?>;