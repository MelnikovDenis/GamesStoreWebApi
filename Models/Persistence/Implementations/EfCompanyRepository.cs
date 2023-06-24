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
            .FirstOrDefaultAsync(c => c.Id == id);
        if (company is null)
            throw new ItemNotFoundException();
        return company;
    }
    public async Task Delete(Company company) { throw new NotImplementedException(); }
    public async Task Update(Company company) { throw new NotImplementedException(); }
    public async Task Create(Company company) { throw new NotImplementedException(); }
}
