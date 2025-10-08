using System.ComponentModel.DataAnnotations;

namespace DDDSample.Application.DTOs;

public class ProductDto
{
    /// <summary>
    /// 商品編號
    /// </summary>
    public int ProductId { get; init; }
    /// <summary>
    /// 商品名稱
    /// </summary>
    [Display(Name = "商品名稱")]
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    /// <summary>
    /// 商品價格
    /// </summary>
    [Display(Name = "商品價格")]
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    /// <summary>
    /// 商品描述
    /// </summary>
    [Display(Name = "商品描述")]
    [Required]
    [MaxLength(500)]
    public required string Description { get; set; }
    /// <summary>
    /// 庫存
    /// </summary>
    [Display(Name = "庫存")]
    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}