using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Members.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Member, MemberDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}