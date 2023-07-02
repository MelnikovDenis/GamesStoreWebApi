namespace GamesStoreWebApi.Models.Persistence.Abstractions;
public interface IGenericRepository<TEntity> where TEntity : class
{
    public Task<int> Count();
    public IQueryable<TEntity> Get();
    public Task<TEntity> GetById(Guid id);
    public void Create(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);
}
