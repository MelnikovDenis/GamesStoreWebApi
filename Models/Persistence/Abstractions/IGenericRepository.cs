namespace GamesStoreWebApi.Models.Persistence.Abstractions;
public interface IGenericRepository<TEntity> where TEntity : class
{
    public Task<int> Count();
    public IQueryable<TEntity> Get();
    public Task<TEntity> GetById(Guid id);
    public Task Create(TEntity entity);
    public Task Update(TEntity entity);
    public Task Delete(Guid id);
}
