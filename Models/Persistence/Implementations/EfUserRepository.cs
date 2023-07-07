using GamesStoreWebApi.Exceptions;
using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GamesStoreWebApi.Models.Persistence.Implementations;

public class EfUserRepository : IGenericRepository<ApplicationUser>
{
    private ApplicationContext _context;

    public EfUserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int> Count()
    {
        return await _context.Users.CountAsync();
    }

    public void Create(ApplicationUser entity)
    {
        _context.Users.Add(entity);
    }

    public void Delete(ApplicationUser entity)
    {
        _context.Users.Remove(entity);
    }

    public IQueryable<ApplicationUser> Get()
    {
        return _context.Users
            .Include(u => u.RefreshTokens)
            .Include(u => u.Purchases)
            .Include(u => u.Collections)
            .AsQueryable();
    }

    public async Task<ApplicationUser> GetById(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.RefreshTokens)
            .Include(u => u.Purchases)
            .Include(u => u.Collections)
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new ItemNotFoundException();
        return user;
    }

    public void Update(ApplicationUser entity)
    {
        _context.Users.Update(entity);
    }
}
