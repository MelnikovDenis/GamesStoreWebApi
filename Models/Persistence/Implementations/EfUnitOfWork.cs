using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;

namespace GamesStoreWebApi.Models.Persistence.Implementations;

public class EfUnitOfWork : IUnitOfWork
{
    private ApplicationContext _context { get; set; }
    public IGenericRepository<Game> GameRepository { get; private set; }
    public IGenericRepository<Company> CompanyRepository { get; private set; }
    public IGenericRepository<Purchase> PurchaseRepository { get; private set; }
    public IGenericRepository<RefreshToken> RefreshTokenRepository { get; private set; }
    public EfUnitOfWork(ApplicationContext context) 
    {
        GameRepository = new EfGameRepository(context);
        CompanyRepository = new EfCompanyRepository(context);
        PurchaseRepository = new EfPurchaseRepository(context);
        RefreshTokenRepository = new EfRefreshTokenRepository(context);
        _context = context;
    }

    public async Task Save()
    {
       await _context.SaveChangesAsync();
    }
}
