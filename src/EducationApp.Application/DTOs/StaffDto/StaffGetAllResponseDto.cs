using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.StaffDto;

public class StaffGetAllResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public Position Role { get; set; } = Position.Other;
}
