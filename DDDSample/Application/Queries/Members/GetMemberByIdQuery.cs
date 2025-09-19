using MediatR;
using DDDSample.Application.DTOs;

public record GetMemberByIdQuery(int Id) : IRequest<MemberDto?>;