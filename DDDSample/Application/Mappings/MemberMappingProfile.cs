using AutoMapper;
using DDDSample.Application.DTOs;
using DDDSample.Domain.Members.Entities;

namespace DDDSample.Application.Mappings;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<Member, MemberDto>().ReverseMap();
    }
}