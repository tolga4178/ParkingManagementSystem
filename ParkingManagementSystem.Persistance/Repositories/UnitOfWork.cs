using ParkingManagementSystem.Application.Repositories;
using ParkingManagementSystem.Application.Repositories.ParkingRecord;
using ParkingManagementSystem.Application.Repositories.Region;
using ParkingManagementSystem.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Persistance.Repositories
{
    public class UnitOfWork: IUnitOfWork, IAsyncDisposable
    {

        private bool _disposed;

        private readonly ParkingManagementSystemContext _context;
        public IParkingRecordReadRepository ParkingRecordRead { get; }
        public IParkingRecordWriteRepository ParkingRecordWrite { get; }
        public IRegionReadRepository RegionRead { get; }
        public IRegionWriteRepository RegionWrite { get; }

        public UnitOfWork(ParkingManagementSystemContext parkingManagementSystemContext,
           IParkingRecordReadRepository parkingRecordRead,
           IParkingRecordWriteRepository parkingRecordWrite,
           IRegionReadRepository regionRead,
           IRegionWriteRepository regionWrite)
        {
            _context = parkingManagementSystemContext;
            ParkingRecordRead = parkingRecordRead;
            ParkingRecordWrite= parkingRecordWrite;
            RegionRead = regionRead;
            RegionWrite = regionWrite;
            _disposed = false;
           
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var affectedRowCount = await _context.SaveChangesAsync(true);
                        await transaction.CommitAsync();
                        return affectedRowCount;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
    
            }

            return default(int);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_disposed)
                return;

            if (_context != null)
            {
                await _context.DisposeAsync();
            }

            _disposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _context?.Dispose();
            }

            _disposed = true;
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();

            Dispose(false);

            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
