using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Domain.Entities;

namespace Motor.Inquiry.Application.Mappings
{
    public class InquiryMappingProfile : Profile
    {
        public InquiryMappingProfile()
        {
            CreateMap<MotorInquiryRequestDto, InquiryHistory>()
                .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Inquiry successful"))
                .ForMember(dest => dest.InquiryDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
