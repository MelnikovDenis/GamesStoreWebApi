using GamesStoreWebApi.Models.Persistence;
using GamesStoreWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using GamesStoreWebApi.Models.ViewModels;
using Microsoft.VisualBasic;

namespace GamesStoreWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private IGamesRepository GamesRepository { get; set; }
    public HomeController(IGamesRepository gamesRepository)
    {
        GamesRepository = gamesRepository;
    }

    [HttpGet]
    public IActionResult GetGamePage(int pageSize, int pageNumber = 1)
    {
        var count = GamesRepository.Count;
        if (pageSize > 0 && pageNumber > 0 && pageSize * pageNumber <= count)
        {
            var pageInfo = new PageViewModel(count, pageSize, pageNumber);
            var games = (from game in GamesRepository.GetGames()
                         select new ListedGameViewModel(
                                game.Id,
                                game.Title,
                                game.Description,
                                (game.Publisher != null ? game.Publisher!.Name : null),
                                (game.Developer != null ? game.Developer!.Name : null),
                                DateOnly.FromDateTime(game.ReleaseDate),
                                (from price in game.Prices where price.StartDate == game.Prices!.Max(p => p.StartDate) select price.Value).FirstOrDefault(),
                                (from discount in game.Discounts where discount.StartDate == game.Discounts!.Max(p => p.StartDate) && discount.EndDate > DateTime.UtcNow select discount.Percent).FirstOrDefault(),
                                (game.Keys != null ? game.Keys.Count() : 0)
                             )
                 )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(new GamePageViewModel(games, pageInfo));
        }
        else 
        {
            return BadRequest("Incorrect page information");
        }
    }
}
