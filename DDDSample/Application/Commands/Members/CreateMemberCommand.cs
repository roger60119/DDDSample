using MediatR;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Commands.Members;

public record CreateMemberCommand(MemberDto Dto) : IRequest<MemberDto>;