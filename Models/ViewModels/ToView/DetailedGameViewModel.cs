using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.ViewModels.Shared;

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
)
{
    public static DetailedGameViewModel FromGame(Game game) 
    {
        var startHistory = DateTime.Today.AddMonths(-6);
        var Now = DateTime.Today;
        return new DetailedGameViewModel(
            game.Id,
            game.Title,
            game.Description,
            game.Publisher is not null ? new CompanyViewModel(game.Publisher.Id, game.Publisher.Name, game.Publisher.Description) : null,
            game.Developer is not null ? new CompanyViewModel(game.Developer.Id, game.Developer.Name, game.Developer.Description) : null,
            DateOnly.FromDateTime(game.ReleaseDate),
            (
                from price in game.Prices
                where price.StartDate >=
                (from underDatePrice in game.Prices where underDatePrice.StartDate <= startHistory select underDatePrice).Max(udp => udp.StartDate)
                select new PriceViewModel(DateOnly.FromDateTime(price.StartDate), price.Value)
            ),
            (from discount in game.Discounts
             where (discount.EndDate >= startHistory && discount.StartDate <= Now)
             select new DiscountViewModel(DateOnly.FromDateTime(discount.StartDate), DateOnly.FromDateTime(discount.EndDate), discount.Percent)),
             (game.Keys != null ? game.Keys.Count : 0)
        );

    }
}