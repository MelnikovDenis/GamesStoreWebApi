using GamesStoreWebApi.Exceptions;
using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GamesStoreWebApi.Models.Persistence.Implementations;

public class EfPurchaseRepository : IGenericRepository<Purchase>
{
    private ApplicationContext _context { get; set; }
    public EfPurchaseRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<int> Count()
    {
        return await _context.Purchases.CountAsync();
    }

    public void Create(Purchase entity)
    {
        _context.Purchases.Add(entity);
    }

    public void Delete(Purchase entity)
    {
        _context.Purchases.Remove(entity);
    }

    public IQueryable<Purchase> Get()
    {
        return _context.Purchases
            .Include(p => p.Purchaser)
            .Include(p => p.Keys)
            .ThenInclude(k => k.KeyGame)
            .AsQueryable();
    }

    public async Task<Purchase> GetById(Guid id)
    {
        var purchase = await _context.Purchases.FirstOrDefaultAsync(p => p.Id == id) 
            ?? throw new ItemNotFoundException();
        return purchase;
    }

    public void Update(Purchase entity)
    {
        _context.Update(entity);
    }
}
