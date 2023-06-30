using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;
public record class CreateUserViewModel(
    [Required, DataType(DataType.EmailAddress), MinLength(2), MaxLength(15)] string UserName,
    [Required, EmailAddress] string Email,
    [Required, DataType(DataType.Password), MinLength(6), MaxLength(25)] string Password
);
