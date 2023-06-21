using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace GamesStoreWebApi.Models.Persistence.Implementations;
public class PricesRepository : IPricesRepository
{
    private ApplicationContext _context { get; set; }
    public PricesRepository(ApplicationContext context)
    {
        _context = context;
    }
    public void CreatePrice(Price price) 
    {
        _context.Prices.Add(price);
        _context.SaveChanges();
    }
}