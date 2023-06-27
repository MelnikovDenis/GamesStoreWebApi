using GamesStoreWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.ViewModels.ToView;
using GamesStoreWebApi.Models.Persistence.Implementations;
using GamesStoreWebApi.Models.ViewModels.FromView;

namespace GamesStoreWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private IGenericRepository<Game> GameRepository { get; set; }
    private IGenericRepository<Company> CompanyRepository { get; set; }
    public GameController(IGenericRepository<Game> gameRepository, IGenericRepository<Company> companyRepository)
    {
        GameRepository = gameRepository;
        CompanyRepository = companyRepository;
    }

    [HttpGet, Route("GetPage")]
    public async Task<IActionResult> GetPage(int pageSize, int pageNumber = 1)
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
    [HttpGet, Route("GetById")]
    public async Task<IActionResult> GetById(Guid id) 
    {
        var game = await GameRepository.GetById(id);
        return Ok(DetailedGameViewModel.FromGame(game));
    }
    [HttpPost, Route("Create")]
    public async Task<IActionResult> Create(CreateGameViewModel createGame)
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
        return RedirectToActionPermanent("GetGame", "Home", new { id = id });
    }
    [HttpDelete, Route("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var game = await GameRepository.GetById(id);
        var gameDetailedViewModel = DetailedGameViewModel.FromGame(game);
        await GameRepository.Delete(game);
        return Ok(gameDetailedViewModel);
    }
    [HttpPut, Route("Update")]
    public async Task<IActionResult> Update(UpdateGameViewModel updateGame)
    {
        var game = await GameRepository.GetById(updateGame.Id);
        game.Title = updateGame.Title;
        game.Description = updateGame.Description;
        game.Publisher = updateGame.PublisherId is not null ? await CompanyRepository.GetById((Guid)updateGame.PublisherId) : null;
        game.Developer = updateGame.DeveloperId is not null ? await CompanyRepository.GetById((Guid)updateGame.DeveloperId) : null;
        game.ReleaseDate = updateGame.ReleaseDate.ToDateTime(TimeOnly.MinValue);
        await GameRepository.Update(game);
        return RedirectToActionPermanent("GetGame", "Home", new { id = updateGame.Id });
    }
}
