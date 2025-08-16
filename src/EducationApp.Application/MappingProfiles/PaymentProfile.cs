using AutoMapper;
using EducationApp.Application.DTOs.PaymentDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payment, PaymentResponseDto>();

        CreateMap<PaymentCreateDto, Payment>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));

        CreateMap<PaymentUpdateDto, Payment>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));
    }
}