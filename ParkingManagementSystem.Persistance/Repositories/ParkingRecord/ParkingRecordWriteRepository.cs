using ParkingManagementSystem.Application.Repositories.ParkingRecord;
using ParkingManagementSystem.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Persistance.Repositories.ParkingRecord
{
    public class ParkingRecordWriteRepository : WriteRepository<ParkingManagementSystem.Domain.ParkingRecord>, IParkingRecordWriteRepository
    {

        public ParkingRecordWriteRepository(ParkingManagementSystemContext dbContext) : base(dbContext) { }
        public decimal CalculateFee(string regionName, TimeSpan parkingDuration)
        {
            decimal baseRate = regionName switch
            {
                "A" => 2m,
                "B" => 3m,
                "C" => 5m,
                _ => 1m
            };

            return baseRate * (decimal)parkingDuration.TotalHours;
        }
    }
}
