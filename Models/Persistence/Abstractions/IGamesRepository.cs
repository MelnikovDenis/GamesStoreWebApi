using GamesStoreWebApi.Models.Entities;

namespace GamesStoreWebApi.Models.Persistence.Abstractions;
public interface IGamesRepository
{
    public int Count { get; }
    public IQueryable<Game> GetGames();
    public void CreateGame(Game game);
}
