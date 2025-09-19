using MediatR;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Commands.Products;

public record CreateProductCommand(ProductDto Dto) : IRequest<ProductDto>;
