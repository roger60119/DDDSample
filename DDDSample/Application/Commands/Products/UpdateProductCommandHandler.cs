using MediatR;
using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Products.Repositories;

namespace DDDSample.Application.Commands.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if (product == null) return false;
        _mapper.Map(request.Dto, product);
        await _repository.UpdateAsync(product);
        return true;
    }
}
