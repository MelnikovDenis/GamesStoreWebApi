using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using GamesStoreWebApi.Exceptions;

namespace GamesStoreWebApi.Models.Persistence.Implementations;

public class EfCompanyRepository : IGenericRepository<Company>
{
    private ApplicationContext _context { get; set; }
    public async Task<int> Count() 
    {
        return await _context.Companies.CountAsync();
    }
    public EfCompanyRepository(ApplicationContext context)
    {
        _context = context;
    }
    public IQueryable<Company> Get() 
    {
        return _context.Companies
            .Include(c => c.PublisherGames)
            .Include(c => c.DeveloperGames)
            .AsQueryable();
    }
    public async Task<Company> GetById(Guid id)
    {
        var company = await _context.Companies
            .Include(c => c.PublisherGames)
            .Include(c => c.DeveloperGames)
            .FirstOrDefaultAsync(c => c.Id == id) 
            ?? throw new ItemNotFoundException();
        return company;
    }
    public void Delete(Company company) 
    { 
        _context.Companies.Remove(company);
    }
    public void Update(Company company) 
    { 
        _context.Companies.Update(company);
    }
    public void Create(Company company) 
    {
        _context.Companies.Add(company);
    }
}
