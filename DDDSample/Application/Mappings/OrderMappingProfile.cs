using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Orders.Entities;
using System;
using System.Runtime.InteropServices;

namespace DDDSample.Application.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}