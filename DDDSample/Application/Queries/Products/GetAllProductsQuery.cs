using MediatR;
using DDDSample.Application.DTOs;
using System.Collections.Generic;

namespace DDDSample.Application.Queries.Products;

public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;
