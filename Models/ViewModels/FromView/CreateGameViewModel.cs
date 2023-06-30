using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;
public record class CreateGameViewModel(
    [Required] string Title,
    string? Description,
    Guid? PublisherId,
    Guid? DeveloperId,
    [Required, Range(0d, 100000d)] decimal StartPrice,
    [Required, DataType(DataType.Date)] DateOnly ReleaseDate,
    IEnumerable<Guid>? Keys
);