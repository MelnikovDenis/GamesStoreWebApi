namespace GamesStoreWebApi.Models.ViewModels.FromView;
public record class CreateGameViewModel(
    string Title,
    string? Description,
    Guid? PublisherId,
    Guid? DeveloperId,
    decimal StartPrice,
    DateOnly ReleaseDate,
    IEnumerable<Guid>? Keys
);