using GamesStoreWebApi.Exceptions;
using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GamesStoreWebApi.Models.Persistence.Implementations;

public class EfRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    private ApplicationContext _context { get; set; }
    public EfRefreshTokenRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int> Count()
    {
        return await _context.RefreshTokens.CountAsync();
    }

    public void Create(RefreshToken entity)
    {
        _context.RefreshTokens.Add(entity);
    }

    public void Delete(RefreshToken entity)
    {
        _context.RefreshTokens.Remove(entity);
    }

    public IQueryable<RefreshToken> Get()
    {
        return _context.RefreshTokens
            .Include(rt => rt.User)
            .AsQueryable();
    }

    public async Task<RefreshToken> GetById(Guid id)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == id) 
            ?? throw new ItemNotFoundException("Invalid refresh token.", HttpStatusCode.Unauthorized);
        return refreshToken;
    }

    public void Update(RefreshToken entity)
    {
        _context.RefreshTokens.Update(entity);
    }
}
