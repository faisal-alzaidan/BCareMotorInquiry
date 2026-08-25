using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection; // 1. أضف هذه المكتبة
using Yaqeen.Application.Interfaces;
using Yaqeen.Domain.Entities;
using Yaqeen.Infrastructure.Persistence;

namespace Yaqeen.Infrastructure.Services
{
    public class VehicleLookupCacheService : IVehicleLookupCacheService
    {
        private readonly IServiceScopeFactory _scopeFactory; // 2. استبدل الـ DbContext بهذا
        private readonly IMemoryCache _memoryCache;
        private const string MakesCacheKey = "VehicleMakes_Cache";

        public VehicleLookupCacheService(IServiceScopeFactory scopeFactory, IMemoryCache memoryCache)
        {
            _scopeFactory = scopeFactory;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<VehicleMake>> GetMakesAsync()
        {
            return await _memoryCache.GetOrCreateAsync(MakesCacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);

                // 3. نفتح Scope مؤقت لجلب البيانات بأمان تام
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<YaqeenDbContext>();

                return await context.VehicleMakes.Include(m => m.Models).ToListAsync();
            }) ?? Enumerable.Empty<VehicleMake>();
        }

        public async Task<IEnumerable<VehicleModel>> GetModelsByMakeIdAsync(int makeId)
        {
            var makes = await GetMakesAsync();
            return makes.FirstOrDefault(m => m.Id == makeId)?.Models ?? Enumerable.Empty<VehicleModel>();
        }
    }
}