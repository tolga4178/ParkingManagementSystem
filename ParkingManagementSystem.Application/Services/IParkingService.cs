using ParkingManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Application.Services
{
    public interface IParkingService
    {
        Task<ParkingRecordDto?> ParkVehicleAsync(string vehicleSize);
        Task<ParkingRecordDto?> ExitVehicleAsync(Guid parkingRecordId);
    }
}
