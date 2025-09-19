using MediatR;

namespace DDDSample.Application.Commands.Products;

public record DeleteProductCommand(int Id) : IRequest<bool>;
