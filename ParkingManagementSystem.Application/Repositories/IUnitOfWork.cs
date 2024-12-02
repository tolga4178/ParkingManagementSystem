using ParkingManagementSystem.Application.Repositories.ParkingRecord;
using ParkingManagementSystem.Application.Repositories.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Application.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        public IParkingRecordReadRepository ParkingRecordRead { get;}
        public IParkingRecordWriteRepository ParkingRecordWrite { get;}
        public IRegionReadRepository RegionRead  { get;}
        public IRegionWriteRepository RegionWrite  { get;}
        public Task<int> SaveAsync();
    }
}
