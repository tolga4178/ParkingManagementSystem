using ParkingManagementSystem.Application.Repositories.ParkingRecord;
using ParkingManagementSystem.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Persistance.Repositories.ParkingRecord
{
    public class ParkingRecordReadRepository: ReadRepository<ParkingManagementSystem.Domain.ParkingRecord>,IParkingRecordReadRepository
    {
        public ParkingRecordReadRepository(ParkingManagementSystemContext dbContext) : base(dbContext) { }
   
    }
}
