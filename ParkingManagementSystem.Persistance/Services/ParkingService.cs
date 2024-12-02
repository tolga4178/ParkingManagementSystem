using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Repositories;
using ParkingManagementSystem.Application.Repositories.ParkingRecord;
using ParkingManagementSystem.Application.Repositories.Region;
using ParkingManagementSystem.Application.Services;
using ParkingManagementSystem.Domain;
using ParkingManagementSystem.Persistance.Repositories;


namespace ParkingManagementSystem.Persistance.Services
{
    public class ParkingService(IUnitOfWork unitOfWork) : IParkingService
    {
        public readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task<ParkingRecordDto?> ParkVehicleAsync(string vehicleSize)
        {
            var regions = await _unitOfWork.RegionRead.GetAllAsync();

            var suitableRegion = regions.FirstOrDefault(r => r.AllowedVehicleSize == vehicleSize && r.OccupiedSpots < r.Capacity);

            if (suitableRegion == null) return null;

            var parkingRecord = new ParkingRecord
            {
                VehicleSize = vehicleSize,
                RegionId = suitableRegion.Id,
                EntryTime = DateTime.UtcNow
            };

            suitableRegion.OccupiedSpots++;

            await _unitOfWork.RegionWrite.UpdateAsync(suitableRegion);
            await _unitOfWork.ParkingRecordWrite.AddAsync(parkingRecord);
            await _unitOfWork.SaveAsync();

            return new ParkingRecordDto
            {
                Id = parkingRecord.Id,
                VehicleSize = vehicleSize,
                RegionName = suitableRegion.Name,
                EntryTime = parkingRecord.EntryTime
            };
        }

        public async Task<ParkingRecordDto?> ExitVehicleAsync(Guid parkingRecordId)
        {
            var record = await _unitOfWork.ParkingRecordRead.GetByIdAsync(parkingRecordId);
            if (record == null || record.ExitTime != null) return null;

            record.ExitTime = DateTime.UtcNow;
            var region = await _unitOfWork.RegionRead.GetByIdAsync(record.RegionId);

            var duration = record.ExitTime.Value - record.EntryTime;
            var fee = _unitOfWork.ParkingRecordWrite.CalculateFee(region.Name, duration);

            region.OccupiedSpots--;
            await _unitOfWork.RegionWrite.UpdateAsync(region);
            await _unitOfWork.ParkingRecordWrite.UpdateAsync(record);
            await _unitOfWork.SaveAsync();

            return new ParkingRecordDto
            {
                Id = record.Id,
                VehicleSize = record.VehicleSize,
                RegionName = region.Name,
                EntryTime = record.EntryTime,
                ExitTime = record.ExitTime,
                Fee = fee
            };
        }
    }
}
