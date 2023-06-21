using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GamesStoreWebApi.Models.Persistence.Implementations;
public class GamesRepository : IGamesRepository
{
    private ApplicationContext _context { get; set; }
    public int Count
    {
        get
        {
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
            .AsQueryable();
    }
    public void CreateGame(Game game)
    {
        _context.Games.Add(game);
        _context.SaveChanges();
    }
}
