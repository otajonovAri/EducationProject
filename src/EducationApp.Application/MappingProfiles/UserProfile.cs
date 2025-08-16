using AutoMapper;
using EducationApp.Application.DTOs.UserDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>()
            .ForAllMembers(opt 
                => opt.Condition((src, dest, srcMember) 
                    => srcMember != null));
    }   
}