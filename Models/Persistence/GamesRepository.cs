using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GamesStoreWebApi.Models.Persistence;
public class GamesRepository : IGamesRepository
{ 
    private ApplicationContext _context { get; set; }   
    public GamesRepository(ApplicationContext context) 
    {
        _context = context;
    }
    public IQueryable<Game> GetGames() 
    {
        return _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Prices)
            .Include(g => g.Discounts)
            .Include(g => g.Keys)
            .AsQueryable<Game>();
    }
}
