using GamesStoreWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.ViewModels.ToView;
using GamesStoreWebApi.Models.ViewModels.FromView;
using Microsoft.AspNetCore.Authorization;

namespace GamesStoreWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private IUnitOfWork UnitOfWork { get; set; }
    public GameController(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    [HttpGet("GetPage")]
    public async Task<IActionResult> GetPage(int pageSize, int pageNumber = 1)
    {
        var pageInfo = new PageInfoViewModel(await UnitOfWork.GameRepository.Count(), pageSize, pageNumber);
        var games = await (from game in UnitOfWork.GameRepository.Get() 
            select SuperficialGameViewModel.FromGame(game)
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
        return Ok(new GenericPageViewModel<SuperficialGameViewModel>(games, pageInfo));
    }
    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(Guid id) 
    {
        var game = await UnitOfWork.GameRepository.GetById(id);
        return Ok(DetailedGameViewModel.FromGame(game));
    }
    [HttpPost("Create"), Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
    public async Task<IActionResult> Create(CreateGameViewModel createGame)
    {
        Guid id = Guid.NewGuid();
        var game = new Game
        {
            Id = id,
            Title = createGame.Title,
            Description = createGame.Description,
            Publisher = createGame.PublisherId is not null ? await UnitOfWork.CompanyRepository.GetById((Guid)createGame.PublisherId) : null,
            Developer = createGame.DeveloperId is not null ? await UnitOfWork.CompanyRepository.GetById((Guid)createGame.DeveloperId) : null,
            ReleaseDate = createGame.ReleaseDate.ToDateTime(TimeOnly.MinValue),
            Prices = new List<Price>() { new Price { PricedGameId = id, Value = createGame.StartPrice } }
        };
        var keys = from keyId in createGame.Keys select new Key { KeyId = keyId, KeyGame = game };
        game.Keys = keys.ToList();
        UnitOfWork.GameRepository.Create(game);
        var gameDetailedViewModel = DetailedGameViewModel.FromGame(game);
        await UnitOfWork.Save();
        return Ok(gameDetailedViewModel);
    }
    [HttpDelete("Delete"), Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var game = await UnitOfWork.GameRepository.GetById(id);
        var gameDetailedViewModel = DetailedGameViewModel.FromGame(game);
        UnitOfWork.GameRepository.Delete(game);
        await UnitOfWork.Save();
        return Ok(gameDetailedViewModel);
    }
    [HttpPut("Update"), Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
    public async Task<IActionResult> Update(UpdateGameViewModel updateGame)
    {
        var game = await UnitOfWork.GameRepository.GetById(updateGame.Id);
        game.Title = updateGame.Title;
        game.Description = updateGame.Description;
        game.Publisher = updateGame.PublisherId is not null ? await UnitOfWork.CompanyRepository.GetById((Guid)updateGame.PublisherId) : null;
        game.Developer = updateGame.DeveloperId is not null ? await UnitOfWork.CompanyRepository.GetById((Guid)updateGame.DeveloperId) : null;
        game.ReleaseDate = updateGame.ReleaseDate.ToDateTime(TimeOnly.MinValue);
        UnitOfWork.GameRepository.Update(game);
        var gameDetailedViewModel = DetailedGameViewModel.FromGame(game);
        await UnitOfWork.Save();
        return Ok(gameDetailedViewModel);
    }
}
