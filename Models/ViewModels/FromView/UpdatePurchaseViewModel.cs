using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;

public record class UpdatePurchaseViewModel(
    [Required] Guid Id,
    [Required]DateTime PurchaseTime, 
    [Required, DataType(DataType.CreditCard)] string Bankcard) { }
