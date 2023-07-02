using GamesStoreWebApi.Exceptions;
using Microsoft.EntityFrameworkCore.Query;
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
#pragma warning disable CS8620 // Аргумент запрещено использовать для параметра из-за различий в отношении допустимости значений NULL для ссылочных типов.
        return _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Prices)
            .Include(g => g.Discounts)
            .Include(g => g.Collections)
            .Include(g => g.Keys)
            .ThenInclude(k => k.KeyPurchase)          
            .AsNoTracking()
            .AsQueryable();
#pragma warning restore CS8620 // Аргумент запрещено использовать для параметра из-за различий в отношении допустимости значений NULL для ссылочных типов.
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
            .FirstOrDefaultAsync(g => g.Id == id) 
            ?? throw new ItemNotFoundException();     
        return game;
    }

    public void Create(Game game)
    {
        _context.Games.Add(game);
    }
    public void Update(Game game)
    {
        _context.Games.Update(game);
    }
    public void Delete(Game game) 
    {
        _context.Games.Remove(game);
    }
}
