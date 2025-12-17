using Application.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TDbContext> : IGenericRepository<TEntity> where TEntity : class where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        public GenericRepository(TDbContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities).ConfigureAwait(false);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetAllWithTracking()
        {
            return _context.Set<TEntity>();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate).ConfigureAwait(false);
        }

        public async ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
        {
            return await _context.Set<TEntity>().FindAsync(keyValues).ConfigureAwait(false);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().AsNoTracking().Where(expression);
        }
        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().UpdateRange(entity);
        }
        /// <summary>
        /// Increase memory usage to increase performance when sorting, grouping, distinct, Count() has I/O to Disk on EXPLAIN ANALYSE
        /// </summary>
        /// <param name="memoryInMb">by default 64Mb</param>
        public void SetWorkMem(int memoryInMb = 64)
        {
            _context.Database.ExecuteSqlRaw($"SET work_mem = '{memoryInMb}MB';");
        }
    }
}
