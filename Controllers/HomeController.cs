using GamesStoreWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.ViewModels.ToView;
using GamesStoreWebApi.Models.ViewModels.FromView;

namespace GamesStoreWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private IGenericRepository<Game> GameRepository { get; set; }
    private IGenericRepository<Company> CompanyRepository { get; set; }
    public HomeController(IGenericRepository<Game> gameRepository, IGenericRepository<Company> companyRepository)
    {
        GameRepository = gameRepository;
        CompanyRepository = companyRepository;
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
        var startHistory = DateTime.Today.AddMonths(-6);
        var Now = DateTime.Today;
        var gameViewModel = new DetailedGameViewModel(
            game.Id,
            game.Title,
            game.Description,
            game.Publisher is not null ? new CompanyViewModel(game.Publisher.Id, game.Publisher.Name, game.Publisher.Description) : null,
            game.Developer is not null ? new CompanyViewModel(game.Developer.Id, game.Developer.Name, game.Developer.Description) : null,
            DateOnly.FromDateTime(game.ReleaseDate),
            (
                from price in game.Prices
                where price.StartDate >=
                (from underDatePrice in game.Prices where underDatePrice.StartDate <= startHistory select underDatePrice).Max(udp => udp.StartDate)
                select new PriceViewModel(DateOnly.FromDateTime(price.StartDate), price.Value)
            ),
            (from discount in game.Discounts
             where (discount.EndDate >= startHistory && discount.StartDate <= Now)
             select new DiscountViewModel(DateOnly.FromDateTime(discount.StartDate), DateOnly.FromDateTime(discount.EndDate), discount.Percent)),
             (game.Keys != null ? game.Keys.Count : 0)
        );
        return Ok(gameViewModel);
    }
    [HttpPost, Route("CreateGame")]
    public async Task<IActionResult> CreateGame(CreateGameViewModel createGame) 
    {        
        Guid id = Guid.NewGuid();
        var game = new Game
        {
            Id = id,
            Title = createGame.Title,
            Description = createGame.Description,
            Publisher = createGame.PublisherId is not null ? await CompanyRepository.GetById((Guid)createGame.PublisherId) : null,
            Developer = createGame.DeveloperId is not null ? await CompanyRepository.GetById((Guid)createGame.DeveloperId) : null,
            ReleaseDate = createGame.ReleaseDate.ToDateTime(TimeOnly.MinValue),
            Prices = new List<Price>() { new Price { PricedGameId = id, Value = createGame.StartPrice } }        
        };
        var keys = from keyId in createGame.Keys select new Key { KeyId = keyId, KeyGame = game };
        game.Keys = keys.ToList();
        await GameRepository.Create(game);
        return RedirectToActionPermanent("GetGame", new {id = id});
    }
}
