namespace GamesStoreWebApi.Models.ViewModels;
public record class DetailedGameViewModel(Guid Id,
        string Title,
        string? Description,
        string? PublisherName,
        string? PublisherDescription,
        string? DeveloperName,
        string? DeveloperDescription,
        DateOnly ReleaseDate,
        IEnumerable<PriceViewModel> PriceHistory,
        IEnumerable<DiscountViewModel>? DiscountHistory,
        int KeyCount
);