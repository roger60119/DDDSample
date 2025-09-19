using MediatR;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Commands.Products;

public record UpdateProductCommand(int Id, ProductDto Dto) : IRequest<bool>;
