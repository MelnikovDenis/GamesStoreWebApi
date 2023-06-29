using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;
public record class CreateUserViewModel(
    [Required, MinLength(2), MaxLength(15)] string UserName,
    [Required, EmailAddress] string Email,
    [Required, MinLength(6), MaxLength(20)] string Password
);
