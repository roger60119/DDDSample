using MediatR;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Queries.Products;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;
