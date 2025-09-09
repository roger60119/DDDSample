using MediatR;
using DDDSample.Application.DTOs;

public record CreateMemberCommand(MemberDto Dto) : IRequest<MemberDto>;