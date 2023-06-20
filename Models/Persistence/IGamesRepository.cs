using GamesStoreWebApi.Models.ViewModels;

namespace GamesStoreWebApi.Models.Persistence;
public interface IGamesRepository
{
    public IEnumerable<ListGameViewModel> GetGames();
}
