using Motor.Inquiry.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Motor.Inquiry.Domain.Entities;
using Motor.Inquiry.Infrastructure.Persistence;

namespace Motor.Inquiry.Infrastructure.Repositories
{
    public class InquiryHistoryRepository : IInquiryHistoryService
    {
        private readonly AppDbContext _context;

        public InquiryHistoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(InquiryHistory history)
        {
            await _context.InquiryHistories.AddAsync(history);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
