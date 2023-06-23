namespace GamesStoreWebApi.Models.ViewModels.ToView;
public record class DiscountViewModel(DateOnly StartDate, DateOnly EndDate, decimal Percent);
