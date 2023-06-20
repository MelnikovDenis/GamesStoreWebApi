namespace GamesStoreWebApi.Models.ViewModels;
public class GamePageViewModel
{
    public IEnumerable<ListedGameViewModel> Games { get; private set; }
    public PageViewModel PageViewModel { get; private set; }
    public GamePageViewModel(IEnumerable<ListedGameViewModel> games, PageViewModel pageViewModel)
    {
        Games = games;
        PageViewModel = pageViewModel;
    }
}

