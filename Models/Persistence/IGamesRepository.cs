using GamesStoreWebApi.Models.Entities;

namespace GamesStoreWebApi.Models.Persistence;
public interface IGamesRepository
{
    public IQueryable<Game> GetGames();
}
