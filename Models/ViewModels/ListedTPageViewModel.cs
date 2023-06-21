namespace GamesStoreWebApi.Models.ViewModels;
public record class ListedTPageViewModel<T>(IEnumerable<T> ListedT, PageViewModel PageViewModel);