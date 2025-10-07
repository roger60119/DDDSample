using DDDSample.Domain.Members.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace DDDSample.Application.DTOs;

public class MemberDto
{
    /// <summary>
    /// �|���s��
    /// </summary>
    public int MemberId { get; init; }
    /// <summary>
    /// �m�W
    /// </summary>
    [Display(Name = "�m�W")]
    [MaxLength(50)]
    [Required]
    public required string Name { get; set; }
    /// <summary>
    /// �q�l�l��
    /// </summary>
    [Display(Name = "�q�l�l��")]
    [EmailAddress]
    [MaxLength(100)]
    [Required]
    public required string Mail { get; set; }
    /// <summary>
    /// �s���q��
    /// </summary>
    [Display(Name = "�p���q��")]
    [Phone]
    [MaxLength(20)]
    [Required]
    public required string MobilePhone { get; set; }
    /// <summary>
    /// �ʧO
    /// </summary>
    [Display(Name = "�ʧO")]
    public Gender Gender { get; set; }
    /// <summary>
    /// �ͤ�
    /// </summary>
    [Display(Name = "�ͤ�")]
    [MaxLength(10)]
    public string? BirthDate { get; set; }
}