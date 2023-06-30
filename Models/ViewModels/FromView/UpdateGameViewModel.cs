using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;
public record class UpdateGameViewModel(
    [Required] Guid Id,
    [Required] string Title,
    string? Description,
    Guid? PublisherId,
    Guid? DeveloperId,
    [DataType(DataType.Date)] DateOnly ReleaseDate
);
