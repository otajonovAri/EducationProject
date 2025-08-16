using System.ComponentModel.DataAnnotations;
using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.PaymentDto;

public class PaymentCreateDto
{
    /* [Required] public int StudentId { get; set; }
     [Required] [Range(0.01, 100000)] public decimal Amount { get; set; }
     [Required] public PaymentType PaymentType { get; set; }
     [StringLength(500)] public string? Description { get; set; }*/
    [Required] public int StudentId { get; set; }
    [Required] public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    [Required] public PaymentType PaymentType { get; set; }
    public string? PaymentImgUrl { get; set; }
    public string? Description { get; set; }
}
