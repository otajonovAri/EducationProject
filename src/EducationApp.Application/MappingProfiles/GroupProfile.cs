using AutoMapper;
using EducationApp.Application.DTOs.GroupDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<Group, GroupResponseDto>();
        CreateMap<GroupCreatedDto, Group>();
        CreateMap<GroupUpdateDto, Group>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));
    }
}