using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Repositories;
using ParkingManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Persistance.Repositories
{
    public class ReadRepository<TEntity>:IReadRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        protected DbSet<TEntity> _dbset => dbContext.Set<TEntity>();

        public DbSet<TEntity> Table => throw new NotImplementedException();

        public ReadRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }

        public virtual IQueryable<TEntity> AsQueryAble() => _dbset.AsQueryable();

        public virtual async Task<List<TEntity>> GetAllAsync(bool noTracking = true)
        {
            var query = _dbset.AsQueryable();
            if (noTracking)
            {
                query.AsNoTracking();
            }
            return await query.ToListAsync();
        }


        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await _dbset.FindAsync(id);

            if (found == null)
            {
                return null;
            }
            if (noTracking)
            {
                _dbset.Entry(found).State = EntityState.Detached;
            }
            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                _dbset.Entry(found).Reference(include).Load();
            }

            return found;
        }


        public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbset;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            query = ApplyIncludes(query, includes);
            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }

        public async virtual Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            return await Get(predicate, noTracking, includes).FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbset.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query;
        }

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {

                foreach (Expression<Func<TEntity, object>> include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }


    }
}
