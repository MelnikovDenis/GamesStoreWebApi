using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;

public record class LoginViewModel([Required, EmailAddress] string Email, [Required, MinLength(6), MaxLength(20)] string Password);