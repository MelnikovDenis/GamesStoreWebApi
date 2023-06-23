namespace GamesStoreWebApi.Models.ViewModels.ToView;
public record class DetailedGameViewModel(
        Guid Id,
        string Title,
        string? Description,
        CompanyViewModel? Publisher,
        CompanyViewModel? Developer,
        DateOnly ReleaseDate,
        IEnumerable<PriceViewModel> PriceHistory,
        IEnumerable<DiscountViewModel>? DiscountHistory,
        int KeyCount
);