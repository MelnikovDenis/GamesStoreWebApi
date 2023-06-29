using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.ViewModels.FromView;
using GamesStoreWebApi.Models.ViewModels.Shared;
using GamesStoreWebApi.Models.ViewModels.ToView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("GetPage")]
        public async Task<IActionResult> GetPage(int pageSize, int pageNumber = 1)
        {
            var pageInfo = new PageInfoViewModel(await GameRepository.Count(), pageSize, pageNumber);
            var companies = await (from company in CompanyRepository.Get()
                select new CompanyViewModel(
                    company.Id, 
                    company.Name,
                    company.Description
                )
            )
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            return Ok(new GenericPageViewModel<CompanyViewModel>(companies, pageInfo));
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await CompanyRepository.GetById(id);
            return Ok(new CompanyViewModel(
                    company.Id,
                    company.Name,
                    company.Description
                )
            );
        }
        [HttpPost("Create"), Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Create(CreateCompanyViewModel createCompany)
        {
            Guid id = Guid.NewGuid();
            var company = new Company
            {
                Id = id,
                Name = createCompany.Name,
                Description = createCompany.Description
            };
            await CompanyRepository.Create(company);
            var companyViewModel = new CompanyViewModel(company.Id, company.Name, company.Description);
            return Ok(companyViewModel);
        }
        [HttpDelete("Delete"), Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var company = await CompanyRepository.GetById(id);
            var companyViewModel = new CompanyViewModel(company.Id, company.Name, company.Description);
            await CompanyRepository.Delete(company);
            return Ok(companyViewModel);
        }
        [HttpPut("Update"), Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Update(CompanyViewModel updateCompany)
        {
            var company = await CompanyRepository.GetById(updateCompany.Id);
            company.Name = updateCompany.Name;
            company.Description = updateCompany.Description;          
            await CompanyRepository.Update(company);
            var companyViewModel = new CompanyViewModel(company.Id, company.Name, company.Description);
            return Ok(companyViewModel);
        }
    }
        
}
