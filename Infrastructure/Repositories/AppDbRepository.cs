using Application.Abstracts.Repositories;
using Infrastructure.Persistance;

namespace Infrastructure.Repositories
{
    public class AppDbRepository<TEntity> : GenericRepository<TEntity, AppDbContext>, IAppDbRepository<TEntity> where TEntity : class
    {
        public AppDbRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
