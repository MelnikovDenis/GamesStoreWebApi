using GamesStoreWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.ViewModels.ToView;

namespace GamesStoreWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private IGenericRepository<Game> GameRepository { get; set; }
    public HomeController(IGenericRepository<Game> gameRepository)
    {
        GameRepository = gameRepository;
    }

    [HttpGet, Route("GetGamesPage")]
    public async Task<IActionResult> GetGamesPage(int pageSize, int pageNumber = 1)
    {
        var pageInfo = new PageViewModel(await GameRepository.Count(), pageSize, pageNumber);
        var games = await (from game in GameRepository.Get() 
            select new SuperficialGameViewModel(
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
        .ToListAsync();
        return Ok(new GenericPageViewModel<SuperficialGameViewModel>(games, pageInfo));
    }
    [HttpGet, Route("GetGame")]
    public async Task<IActionResult> GetGame(Guid id) 
    {
        var game = await GameRepository.GetById(id);
        return Ok(DetailedGameViewModel.FromGame(game));
    }
   
}
