using GamesStoreWebApi.Models.Entities; 

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
)
{
    public static SuperficialGameViewModel FromGame(Game game) 
    {
        return new SuperficialGameViewModel(
            game.Id,
            game.Title,
            game.Description,
            (game.Publisher != null ? game.Publisher!.Name : null),
            (game.Developer != null ? game.Developer!.Name : null),
            DateOnly.FromDateTime(game.ReleaseDate),
            (from price in game.Prices where price.StartDate == game.Prices!.Max(p => p.StartDate) select price.Value).FirstOrDefault(),
            (from discount in game.Discounts where discount.StartDate == game.Discounts!.Max(p => p.StartDate) && discount.EndDate > DateTime.Today select discount.Percent).FirstOrDefault(),
            (game.Keys != null ? game.Keys.Count : 0)
        );
    }
};
