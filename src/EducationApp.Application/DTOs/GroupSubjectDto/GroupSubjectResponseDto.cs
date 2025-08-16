namespace EducationApp.Application.DTOs.GroupSubjectDto;

public class GroupSubjectResponseDto
{
    /*public int Id { get; set; }
    public int GroupId { get; set; }
    public string? GroupName { get; set; }
    public int SubjectId { get; set; }
    public string? SubjectName { get; set; }
    public int StaffId { get; set; }
    public string? StaffName { get; set; }
    public int RoomId { get; set; }
    public string? RoomNumber { get; set; }
    public DateTime SessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }*/
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int SubjectId { get; set; }
    public int StaffId { get; set; }
    public int RoomId { get; set; }
    public DateTime SessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}