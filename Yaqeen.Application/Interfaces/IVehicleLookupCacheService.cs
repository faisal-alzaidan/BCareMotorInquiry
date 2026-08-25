using Yaqeen.Domain.Entities;

namespace Yaqeen.Application.Interfaces
{
    public interface IVehicleLookupCacheService
    {
        // IEnumerable means return an read-only collection of VehicleMake and VehicleModel objects, which can be enumerated over.
        Task<IEnumerable<VehicleMake>> GetMakesAsync();
        Task<IEnumerable<VehicleModel>> GetModelsByMakeIdAsync(int makeId);
    }
}