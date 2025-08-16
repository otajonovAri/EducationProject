using AutoMapper;
using EducationApp.Application.DTOs.StaffSubjectDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class StaffSubjectProfile : Profile
{
    public StaffSubjectProfile()
    {
        CreateMap<StaffSubject, StaffSubjectResponseDto>();
        CreateMap<StaffSubjectCreateDto, StaffSubject>();
        CreateMap<StaffSubjectUpdateDto, StaffSubject>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}