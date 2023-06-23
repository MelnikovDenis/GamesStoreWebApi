namespace GamesStoreWebApi.Models.ViewModels.ToView;
public record class GenericPageViewModel<T>(IEnumerable<T> ListedT, PageViewModel PageViewModel);