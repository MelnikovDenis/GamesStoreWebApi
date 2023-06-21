namespace GamesStoreWebApi.Models.ViewModels;
public record class ListedGameViewModel(Guid Id,
    string Title,
    string? Description,
    string? PublisherName,
    string? DeveloperName,
    DateOnly ReleaseDate,
    decimal CurrentPrice,
    decimal? CurrentDiscount,
    int KeyCount
);
