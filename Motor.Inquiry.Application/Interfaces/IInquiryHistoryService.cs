using Motor.Inquiry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.Interfaces
{
    public interface IInquiryHistoryService
    {
        Task AddAsync(InquiryHistory history);
        Task SaveChangesAsync();
    }
}
