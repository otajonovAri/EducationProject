using AutoMapper;
using EducationApp.Application.DTOs.GroupDto;
using EducationApp.Application.DTOs.PaymentDocumentDto;
using EducationApp.Core.Entities;

namespace EducationApp.Application.MappingProfiles;

public class PaymentDocumentProfile : Profile
{
    public PaymentDocumentProfile()
    {
        CreateMap<PaymentDocument, PaymentDocumentResponseDto>();
        CreateMap<PaymentDocumentCreateDto, PaymentDocument>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));
        CreateMap<PaymentDocumentUpdateDto, PaymentDocument>()
            .ForAllMembers(opt
                => opt.Condition((src, dest, srcMember)
                    => srcMember != null));
    }   
}