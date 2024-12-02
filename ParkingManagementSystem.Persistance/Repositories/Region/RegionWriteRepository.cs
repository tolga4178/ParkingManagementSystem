using ParkingManagementSystem.Application.Repositories.Region;
using ParkingManagementSystem.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Persistance.Repositories.Region
{
    public class RegionWriteRepository: WriteRepository<ParkingManagementSystem.Domain.Region>, IRegionWriteRepository
    {
        public RegionWriteRepository(ParkingManagementSystemContext dbContext) : base(dbContext) { }
    }
}
