using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.PaymentDocumentDto;

public record PaymentDocumentCreateDto
{
    /* [Required] public string? FileUrl { get; set; }
     [Required] public int PaymentId { get; set; }*/
    [Required] public int PaymentId { get; set; }
    public string? FileUrl { get; set; }
}