using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GamesStoreWebApi.Models.Persistence.Implementations;
public class EfGameRepository : IGenericRepository<Game>
{
    private ApplicationContext _context { get; set; }
    public async Task<int> Count() 
    {
        return await _context.Games.CountAsync();
    }
    public EfGameRepository(ApplicationContext context)
    {
        _context = context;
    }
    public IQueryable<Game> Get()
    {
        return _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Prices)
            .Include(g => g.Discounts)
            .Include(g => g.Keys)
            .Include(g => g.Collections)
            .AsNoTracking()
            .AsQueryable();            
    }
    public async Task<Game> GetById(Guid id) 
    {
        var game = await _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Prices)
            .Include(g => g.Discounts)
            .Include(g => g.Keys)
            .Include(g => g.Collections)
            .FirstOrDefaultAsync(g => g.Id == id);
        if(game is null)
            throw new ArgumentOutOfRangeException("Invalid id");
        return game;
    }

    public async Task Create(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
    }
    public async Task Update(Game game) { throw  new NotImplementedException(); }
    public async Task Delete(Game game) 
    {
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }
}
