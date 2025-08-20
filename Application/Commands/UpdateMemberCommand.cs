using MediatR;
using DDDSample.Application.DTOs;

public record UpdateMemberCommand(int Id, MemberDto Dto) : IRequest<bool>;