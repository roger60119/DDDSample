using DDDSample.Domain.Members.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace DDDSample.Application.DTOs;

public class MemberDto
{
    /// <summary>
    /// 會員編號
    /// </summary>
    public int MemberId { get; init; }
    /// <summary>
    /// 姓名
    /// </summary>
    [Display(Name = "姓名")]
    [MaxLength(50)]
    [Required]
    public required string Name { get; set; }
    /// <summary>
    /// 電子郵件
    /// </summary>
    [Display(Name = "電子郵件")]
    [EmailAddress]
    [MaxLength(100)]
    [Required]
    public required string Mail { get; set; }
    /// <summary>
    /// 連絡電話
    /// </summary>
    [Display(Name = "聯絡電話")]
    [Phone]
    [MaxLength(20)]
    [Required]
    public required string MobilePhone { get; set; }
    /// <summary>
    /// 性別
    /// </summary>
    [Display(Name = "性別")]
    public Gender Gender { get; set; }
    /// <summary>
    /// 生日
    /// </summary>
    [Display(Name = "生日")]
    [MaxLength(10)]
    public string? BirthDate { get; set; }
}