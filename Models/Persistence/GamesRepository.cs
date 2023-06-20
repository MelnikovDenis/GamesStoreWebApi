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
    public IEnumerable<ListGameViewModel> GetGames() 
    {
        return (from game
            in _context.Games.Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Prices)
            .Include(g => g.Discounts)
            select new ListGameViewModel(
                game.Id,
                game.Title,
                game.Description,
                (game.Publisher == null ? game.Publisher.Name : null),
                (game.Publisher == null ? game.Publisher.Name : null),
                game.ReleaseDate,
                (from price in game.Prices where price.StartDate == game.Prices.Max(p => p.StartDate) select price.Value).First(),
                null
            )
        ).ToList();
    }
}
