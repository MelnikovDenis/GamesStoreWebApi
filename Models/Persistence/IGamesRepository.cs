using GamesStoreWebApi.Models.Entities;

namespace GamesStoreWebApi.Models.Persistence;
public interface IGamesRepository
{
    public int Count { get; }
    public IQueryable<Game> GetGames();
    public Game? GetGame(Guid id);
}
