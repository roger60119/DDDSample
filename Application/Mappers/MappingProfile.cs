using AutoMapper;
using DDDSample.Domain.Entities;
using DDDSample.Application.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Member, MemberDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}