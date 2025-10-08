using MediatR;

namespace DDDSample.Application.Commands.Products
{
    public record UpdateProductStockCommand(int Id, int Stock) : IRequest<bool>;
}
