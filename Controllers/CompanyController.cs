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
        private IUnitOfWork UnitOfWork { get; set; }
        public CompanyController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        [HttpGet("GetPage")]
        public async Task<IActionResult> GetPage(int pageSize, int pageNumber = 1)
        {
            var pageInfo = new PageInfoViewModel(await UnitOfWork.GameRepository.Count(), pageSize, pageNumber);
            var companies = await (from company in UnitOfWork.CompanyRepository.Get()
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
            var company = await UnitOfWork.CompanyRepository.GetById(id);
            return Ok(new CompanyViewModel(
                    company.Id,
                    company.Name,
                    company.Description
                )
            );
        }
        [HttpPost("Create"), Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        public async Task<IActionResult> Create(CreateCompanyViewModel createCompany)
        {
            Guid id = Guid.NewGuid();
            var company = new Company
            {
                Id = id,
                Name = createCompany.Name,
                Description = createCompany.Description
            };
            UnitOfWork.CompanyRepository.Create(company);
            var companyViewModel = new CompanyViewModel(company.Id, company.Name, company.Description);
            await UnitOfWork.Save();
            return Ok(companyViewModel);
        }
        [HttpDelete("Delete"), Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var company = await UnitOfWork.CompanyRepository.GetById(id);
            var companyViewModel = new CompanyViewModel(company.Id, company.Name, company.Description);
            UnitOfWork.CompanyRepository.Delete(company);
            await UnitOfWork.Save();
            return Ok(companyViewModel);
        }
        [HttpPut("Update"), Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        public async Task<IActionResult> Update(CompanyViewModel updateCompany)
        {
            var company = await UnitOfWork.CompanyRepository.GetById(updateCompany.Id);
            company.Name = updateCompany.Name;
            company.Description = updateCompany.Description;
            UnitOfWork.CompanyRepository.Update(company);
            var companyViewModel = new CompanyViewModel(company.Id, company.Name, company.Description);
            await UnitOfWork.Save();
            return Ok(companyViewModel);
        }
    }
        
}
