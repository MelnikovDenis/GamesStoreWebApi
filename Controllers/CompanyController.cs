using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.ViewModels.FromView;
using GamesStoreWebApi.Models.ViewModels.ToView;
using Microsoft.AspNetCore.Mvc;

namespace GamesStoreWebApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private IGenericRepository<Company> CompanyRepository { get; set; }
        private IGenericRepository<Game> GameRepository { get; set; }
        public CompanyController(IGenericRepository<Game> gameRepository, IGenericRepository<Company> companyRepository)
        {
            GameRepository = gameRepository;
            CompanyRepository = companyRepository;
        }

        
    }
}
