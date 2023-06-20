using GamesStoreWebApi.Models.Persistence;
using GamesStoreWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetGames()
    {
        return Ok(GamesRepository.GetGames());
    }
}
