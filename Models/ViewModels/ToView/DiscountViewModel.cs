using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.ToView;
public record class DiscountViewModel([DataType(DataType.Date)] DateOnly StartDate, [DataType(DataType.Date)] DateOnly EndDate, decimal Percent);
