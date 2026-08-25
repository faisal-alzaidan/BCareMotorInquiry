using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Application.Interfaces;
using Motor.Inquiry.Domain.Entities;
using Motor.Inquiry.Infrastructure.Persistence;

namespace Motor.Inquiry.Infrastructure.Repositories
{
    public class VehicleInquiryCacheRepository : IVehicleInquiryCacheRepository
    {
        private readonly AppDbContext _context;

        public VehicleInquiryCacheRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleDetailsDto?> GetCachedResponseAsync(string? sequenceNumber, string? plateLetters, string? plateNumber)
        {
            var cacheKey = !string.IsNullOrEmpty(sequenceNumber)
                ? $"seq_{sequenceNumber}"
                : $"plate_{plateLetters}_{plateNumber}";

            var cached = await _context.VehicleInquiryCaches
                .FirstOrDefaultAsync(c => c.CacheKey == cacheKey);

            if (cached != null && cached.InquiryDate >= DateTime.UtcNow.AddDays(-30))
            {
                return JsonSerializer.Deserialize<VehicleDetailsDto>(cached.ResponseJson);
            }

            return null;
        }

        public async Task SetCacheAsync(string? sequenceNumber, string? plateLetters, string? plateNumber, VehicleDetailsDto vehicleDetails)
        {
            var jsonResponse = JsonSerializer.Serialize(vehicleDetails);

            var cacheKey = !string.IsNullOrEmpty(sequenceNumber)
                ? $"seq_{sequenceNumber}"
                : $"plate_{plateLetters}_{plateNumber}";

            var cached = await _context.VehicleInquiryCaches
                .FirstOrDefaultAsync(c => c.CacheKey == cacheKey);

            if (cached != null)
            {
                cached.ResponseJson = jsonResponse;
                cached.InquiryDate = DateTime.UtcNow;
            }
            else
            {
                _context.VehicleInquiryCaches.Add(new VehicleInquiryCache
                {
                    CacheKey = cacheKey,
                    SequenceNumber = sequenceNumber,
                    PlateLetters = plateLetters,
                    PlateNumber = plateNumber,
                    ResponseJson = jsonResponse,
                    InquiryDate = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}