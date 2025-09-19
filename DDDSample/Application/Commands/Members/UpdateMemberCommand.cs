using MediatR;
using DDDSample.Application.DTOs;

namespace DDDSample.Application.Commands.Members;

public record UpdateMemberCommand(int Id, MemberDto Dto) : IRequest<bool>;