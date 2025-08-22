namespace DDDSample.Application.DTOs;

public class MemberDto
{
    public int Id { get; private set; }
    public string Name { get; set; } = null!;
    public string Mail { get; set; } = null!;
}