using EducationApp.Core.Enums;

namespace EducationApp.Application.DTOs.GroupDto;

public class GroupGetAllResponseDto
{
    public int Id { get; set; }
    public int StudentNumber { get; set; }
    public string GroupName { get; set; }
    public string TeacherName { get; set; }
    public TimeSpan StartTime { get; set; }
    public GroupDays Groupdays { get; set; }
}
