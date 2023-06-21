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

    [HttpGet, Route("GetGameListPage")]
    public IActionResult GetGameListPage(int pageSize, int pageNumber = 1)
    {
        var count = GamesRepository.Count;
        if (pageSize > 0 && pageNumber > 0 && pageSize * pageNumber <= count)
        {
            var pageInfo = new PageViewModel(count, pageSize, pageNumber);
            var gamesViewModel = (from game in GamesRepository.GetGames()
                         select new ListedGameViewModel(
                                game.Id,
                                game.Title,
                                game.Description,
                                (game.Publisher != null ? game.Publisher!.Name : null),
                                (game.Developer != null ? game.Developer!.Name : null),
                                DateOnly.FromDateTime(game.ReleaseDate),
                                (from price in game.Prices where price.StartDate == game.Prices!.Max(p => p.StartDate) select price.Value).FirstOrDefault(),
                                (from discount in game.Discounts where discount.StartDate == game.Discounts!.Max(p => p.StartDate) && discount.EndDate > DateTime.Today select discount.Percent).FirstOrDefault(),
                                (game.Keys != null ? game.Keys.Count() : 0)
                             )
                 )
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(new GamePageViewModel(gamesViewModel, pageInfo));
        }
        else 
        {
            return BadRequest("Incorrect page information");
        }
    }
    [HttpGet, Route("GetGame")]
    public IActionResult GetGame(Guid id) 
    {
        var game = GamesRepository.GetGame(id);
        if(game is not null) 
        {
            var startHistory = DateTime.Today.AddMonths(-6);
            var Now = DateTime.Today;
            var gameViewModel = new DetailedGameViewModel(game.Id,
                game.Title,
                game.Description,
                game.Publisher?.Name,
                game.Publisher?.Description,
                game.Developer?.Name,
                game.Developer?.Description,
                DateOnly.FromDateTime(game.ReleaseDate),
                (from price in game.Prices 
                    where price.StartDate >=
                    (from underDatePrice in game.Prices where underDatePrice.StartDate <= startHistory select underDatePrice)
                        .Max(udp => udp.StartDate)
                    select new PriceViewModel(DateOnly.FromDateTime(price.StartDate), price.Value)                    
                ),
                (from discount in game.Discounts
                 where (discount.EndDate >= startHistory && discount.StartDate <= Now)
                 select new DiscountViewModel(DateOnly.FromDateTime(discount.StartDate), DateOnly.FromDateTime(discount.EndDate), discount.Percent)),
                 (game.Keys != null ? game.Keys.Count : 0)
            );
            return Ok(gameViewModel);
        }
        else 
        {
            return BadRequest("Incorrect Id");
        }
    }
}
