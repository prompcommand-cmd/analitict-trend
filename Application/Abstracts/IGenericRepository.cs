using System.Linq.Expressions;

namespace Application.Abstracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllWithTracking();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        ValueTask<TEntity?> FindAsync(params object?[]? keyValues);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Increase memory usage to increase performance when sorting, grouping, distinct, Count() has I/O to Disk on EXPLAIN ANALYSE
        /// make sure 
        /// </summary>
        /// <param name="memoryInMb">by default 64Mb</param>
        void SetWorkMem(int memoryInMb = 64);
    }
}
