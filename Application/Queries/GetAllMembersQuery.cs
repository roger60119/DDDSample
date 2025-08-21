using MediatR;
using DDDSample.Application.DTOs;
using System.Collections.Generic;

public record GetAllMembersQuery() : IRequest<IEnumerable<MemberDto>>;