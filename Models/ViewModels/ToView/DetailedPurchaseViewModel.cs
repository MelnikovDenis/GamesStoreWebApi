using GamesStoreWebApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.ToView;

public record class DetailedPurchaseViewModel(
    List<PurchaseGamePriceViewModel> Games, 
    DateTime PurchaseTime, 
    [DataType(DataType.CreditCard)]string BankCard
);
public record class PurchaseGamePriceViewModel(
    Guid GameId, 
    string GameTitle, 
    decimal Price, 
    List<Guid> GameKeys
)
{
    public static PurchaseGamePriceViewModel FromGame(Game game, List<Key> keys)
    {
        return 
            new PurchaseGamePriceViewModel
            (
                GameId: game.Id,
                GameTitle: game.Title,
                Price: (from price in game.Prices
                        where price.StartDate == game.Prices!.Max(p => p.StartDate)
                        select price).FirstOrDefault()!.Value,
                GameKeys: (from key in keys select key.KeyId).ToList()
            );
    }
}