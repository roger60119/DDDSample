using MediatR;
using DDDSample.Application.DTOs;

public record GetOrderByIdQuery(long Id) : IRequest<OrderDto?>;