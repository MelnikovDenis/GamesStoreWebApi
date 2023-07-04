using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;

public record class CreatePurchaseViewModel(
    [Required] Dictionary<Guid, int> GameKeyCountDict, 
    [Required, DataType(DataType.CreditCard)]string Bankcard) { }