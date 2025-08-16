using AutoMapper;
using EducationApp.Application.DTOs.SubjectDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, SubjectResponseDto>();
        CreateMap<CreateSubjectDto, Subject>();
        CreateMap<SubjectUpdateDto, Subject>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));
    }
}