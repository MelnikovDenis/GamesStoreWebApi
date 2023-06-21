namespace GamesStoreWebApi.Models.ViewModels;
public record class DiscountViewModel(DateOnly StartDate, DateOnly EndDate, decimal Percent);
