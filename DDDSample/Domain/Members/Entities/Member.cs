using DDDSample.Domain.Members.Enumerations;

namespace DDDSample.Domain.Members.Entities;

public class Member
{
    public int MemberId { get; init; }
    public required string Name { get; set; }
    public required string Mail { get; set; }
    public required string MobilePhone { get; set; }
    public Gender Gender { get; set; }
    public string? BirthDate { get; set; }
}
