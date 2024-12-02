using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Application.Repositories.ParkingRecord
{
    public interface IParkingRecordWriteRepository: IWriteRepository<ParkingManagementSystem.Domain.ParkingRecord>
    {
        decimal CalculateFee(string regionName, TimeSpan parkingDuration);
    }
}
