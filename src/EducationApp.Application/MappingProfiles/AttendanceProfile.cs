using AutoMapper;
using EducationApp.Application.DTOs.AttendanceDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class AttendanceProfile : Profile
{
    public AttendanceProfile()
    {
        CreateMap<Attendance, AttendanceResponseDto>();
        CreateMap<AttendanceCreateDto, Attendance>();
        CreateMap<AttendanceUpdateDto, Attendance>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));
    }   
}