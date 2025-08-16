using AutoMapper;
using EducationApp.Application.DTOs.GroupDto;
using EducationApp.Application.DTOs.StaffDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class StaffProfile : Profile
{
    public StaffProfile()
    {
        CreateMap<Staff, StaffResponseDto>();
        CreateMap<StaffCreateDto, Staff>();
        CreateMap<StaffUpdateDto, Staff>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));
    }
}