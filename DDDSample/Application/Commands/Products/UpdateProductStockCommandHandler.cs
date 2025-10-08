using DDDSample.Domain.Products.Repositories;
using MediatR;

namespace DDDSample.Application.Commands.Products
{
    public class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand, bool>
    {
        private IProductRepository _productRepository;

        public UpdateProductStockCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var product =  await _productRepository.GetByIdAsync(request.Id);
            if (product == null) return false;
            product.Stock = request.Stock;
            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}
