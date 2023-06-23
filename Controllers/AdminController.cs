﻿using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.Persistence.Implementations;
using GamesStoreWebApi.Models.ViewModels.FromView;
using GamesStoreWebApi.Models.ViewModels.ToView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesStoreWebApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private IGenericRepository<Company> CompanyRepository { get; set; }
        private IGenericRepository<Game> GameRepository { get; set; }
        public AdminController(IGenericRepository<Game> gameRepository, IGenericRepository<Company> companyRepository)
        {
            GameRepository = gameRepository;
            CompanyRepository = companyRepository;
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
            return RedirectToActionPermanent("GetGame", "Home", new { id = id });
        }
        [HttpPost, Route("DeleteGame")]
        public async Task<IActionResult> DeleteGame(Guid id) 
        {
            var game = await GameRepository.GetById(id);
            var gameDetailedViewModel = DetailedGameViewModel.FromGame(game);
            await GameRepository.Delete(game);
            return Ok(gameDetailedViewModel);
        }
    }
}