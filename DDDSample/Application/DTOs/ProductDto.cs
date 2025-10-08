using System.ComponentModel.DataAnnotations;

namespace DDDSample.Application.DTOs;

public class ProductDto
{
    /// <summary>
    /// �ӫ~�s��
    /// </summary>
    public int ProductId { get; init; }
    /// <summary>
    /// �ӫ~�W��
    /// </summary>
    [Display(Name = "�ӫ~�W��")]
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    /// <summary>
    /// �ӫ~����
    /// </summary>
    [Display(Name = "�ӫ~����")]
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    /// <summary>
    /// �ӫ~�y�z
    /// </summary>
    [Display(Name = "�ӫ~�y�z")]
    [Required]
    [MaxLength(500)]
    public required string Description { get; set; }
    /// <summary>
    /// �w�s
    /// </summary>
    [Display(Name = "�w�s")]
    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}