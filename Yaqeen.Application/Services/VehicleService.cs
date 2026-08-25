

using System.Threading.Tasks;
using Yaqeen.Application.Common;
using Yaqeen.Application.Common.Exceptions;
using Yaqeen.Application.DTOs;
using Yaqeen.Application.Interfaces;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ApiResponse<VehicleDetailsDto>> GetBySequenceNumberAsync(string sequenceNumber)
        {
            var vehicle = await _vehicleRepository.GetBySequenceNumberAsync(sequenceNumber);

            if (vehicle == null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            var dto = MapToDto(vehicle);
            return CreateResponse(true, "Vehicle retrieved successfully.", dto);
        }

        public async Task<ApiResponse<VehicleDetailsDto>> GetByPlateAsync(GetVehicleByPlateRequestDto request)
        {
            var vehicle = await _vehicleRepository.GetByPlateAsync(request.PlateNumber, request.PlateLetters);

            if (vehicle == null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            var dto = MapToDto(vehicle);
            return CreateResponse(true, "Vehicle retrieved successfully.", dto);
        }



        public async Task<ApiResponse<bool>> VerifyOwnershipAsync(string sequenceNumber, string nationalId)
        {
            var isOwner = await _vehicleRepository.VerifyOwnershipAsync(sequenceNumber, nationalId);
            string message = isOwner ? "Ownership verified successfully." : "Vehicle ownership does not match.";

            return CreateResponse(true, message, isOwner);
        }

        private static VehicleDetailsDto MapToDto(Vehicles vehicle)
        {
            return new VehicleDetailsDto
            {
                SequenceNumber = vehicle.SequenceNumber,
                ModelYear = vehicle.ModelYear,
                Color = vehicle.Color,
                PlateNumber = vehicle.PlateNumber,
                PlateLetters = vehicle.PlateLetters,
                OwnerNationalId = vehicle.OwnerNationalId,

                // [إضافة جديدة]: تحويل كائن الماركة (Make) إلى DTO الخاص به بأمان مع فحص الـ Null
                Make = vehicle.Make != null ? new VehicleMakeDto
                {
                    Id = vehicle.Make.Id,
                    NameAr = vehicle.Make.NameAr,
                    NameEn = vehicle.Make.NameEn
                } : null,

                // [إضافة جديدة]: تحويل كائن الموديل (Model) إلى DTO الخاص به بأمان مع فحص الـ Null
                Model = vehicle.Model != null ? new VehicleModelDto
                {
                    Id = vehicle.Model.Id,
                    NameAr = vehicle.Model.NameAr,
                    NameEn = vehicle.Model.NameEn
                } : null
            };
        }

        private static ApiResponse<T> CreateResponse<T>(bool success, string message, T data)
        {
            return new ApiResponse<T>
            {
                Success = success,
                Message = message,
                Data = data
            };
        }
    }
}