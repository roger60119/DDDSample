using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Orders.Entities;

namespace DDDSample.Application.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}