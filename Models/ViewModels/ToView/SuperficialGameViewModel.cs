namespace GamesStoreWebApi.Models.ViewModels.ToView;
public record class SuperficialGameViewModel(Guid Id,
    string Title,
    string? Description,
    string? PublisherName,
    string? DeveloperName,
    DateOnly ReleaseDate,
    decimal CurrentPrice,
    decimal? CurrentDiscount,
    int KeyCount
);
