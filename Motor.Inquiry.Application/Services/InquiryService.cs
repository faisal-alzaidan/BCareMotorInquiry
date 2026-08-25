using AutoMapper;
using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Application.Interfaces;
using Motor.Inquiry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly IInquiryHistoryService _historyRepo;
        private readonly IMapper _mapper;

        public InquiryService(IInquiryHistoryService historyRepo, IMapper mapper)
        {
            _historyRepo = historyRepo;
            _mapper = mapper;
        }

        public async Task ProcessInquiryAsync (MotorInquiryRequestDto request)
        {
            var history = _mapper.Map<InquiryHistory>(request);

            await _historyRepo.AddAsync(history);
            await _historyRepo.SaveChangesAsync();
        }
    }
}
