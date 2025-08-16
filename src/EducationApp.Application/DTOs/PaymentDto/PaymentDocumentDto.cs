using System.ComponentModel.DataAnnotations;

namespace EducationApp.Application.DTOs.PaymentDto;

public class PaymentDocumentDto
{ 
    public string? FileUrl { get; set; }
    public int PaymentId { get; set; }
}