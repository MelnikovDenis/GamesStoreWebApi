using GamesStoreWebApi.Models.Entities;
namespace GamesStoreWebApi.Models.Persistence.Abstractions;

public interface IUnitOfWork
{
    public IGenericRepository<Game> GameRepository { get; }
    public IGenericRepository<Company> CompanyRepository { get; }
    public IGenericRepository<Purchase> PurchaseRepository { get; }
    public Task Save();
}
