namespace Application.Abstracts.Repositories
{
    public interface IAppDbRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
    }
}
