using Motor.Inquiry.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Motor.Inquiry.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<InquiryHistory> InquiryHistories { get; set; } = null!;
    }
}
