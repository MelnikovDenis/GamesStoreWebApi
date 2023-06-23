namespace GamesStoreWebApi.Models.ViewModels.FromView;
public record class UpdateGameViewModel(
    Guid Id,
    string Title,
    string? Description,
    Guid? PublisherId,
    Guid? DeveloperId,
    DateOnly ReleaseDate
);
