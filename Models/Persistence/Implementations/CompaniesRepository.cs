using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesStoreWebApi.Models.Persistence.Implementations;

public class CompaniesRepository : ICompaniesRepository
{
    private ApplicationContext _context { get; set; }
    public CompaniesRepository(ApplicationContext context)
    {
        _context = context;
    }
    public IQueryable<Company> GetCompanies() 
    {
        return _context.Companies.AsQueryable<Company>();
    }
}
