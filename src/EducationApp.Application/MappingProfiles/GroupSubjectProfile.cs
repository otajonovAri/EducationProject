using AutoMapper;
using EducationApp.Application.DTOs.GroupSubjectDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class GroupSubjectProfile : Profile
{
    public GroupSubjectProfile()
    {
        CreateMap<GroupSubject, GroupSubjectResponseDto>();
        CreateMap<GroupSubjectCreateDto, GroupSubject>();
        CreateMap<GroupSubjectUpdateDto, GroupSubject>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));
    }
}