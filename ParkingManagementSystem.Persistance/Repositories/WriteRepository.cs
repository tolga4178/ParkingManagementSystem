using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Repositories;
using ParkingManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Persistance.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        protected DbSet<TEntity> _dbset => dbContext.Set<TEntity>();

        public WriteRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }
 
        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbset.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }
            _dbset.Remove(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _dbset.FindAsync(id);
            await DeleteAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbset.Attach(entity);
            _dbset.Entry(entity).State = EntityState.Modified;
        }
    }
}
