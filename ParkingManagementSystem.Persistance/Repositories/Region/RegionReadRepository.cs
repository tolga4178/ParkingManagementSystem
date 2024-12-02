using ParkingManagementSystem.Application.Repositories.Region;
using ParkingManagementSystem.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Persistance.Repositories.Region
{
    public class RegionReadRepository: ReadRepository<ParkingManagementSystem.Domain.Region>, IRegionReadRepository
    {
        public RegionReadRepository(ParkingManagementSystemContext dbContext) : base(dbContext) { }
    }
}
