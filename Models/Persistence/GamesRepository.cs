using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GamesStoreWebApi.Models.Persistence;
public class GamesRepository : IGamesRepository
{ 
    private ApplicationContext _context { get; set; }   
    public int Count { get {
            return _context.Games.AsNoTracking().Count();
        } 
    }
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
            .AsNoTracking()
            .AsQueryable<Game>();
    }
    public Game? GetGame(Guid id) 
    {
        return _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Prices)
            .Include(g => g.Discounts)
            .Include(g => g.Keys)
            .AsNoTracking()
            .First(g => g.Id == id);
    }
}
