using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.ToView;
public record class PriceViewModel([DataType(DataType.Date)] DateOnly StartDate, decimal Value);
