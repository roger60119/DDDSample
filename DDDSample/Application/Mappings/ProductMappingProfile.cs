using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Products.Entities;

namespace DDDSample.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}