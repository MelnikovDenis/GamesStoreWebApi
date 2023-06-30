using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;

public record class LoginViewModel([Required, EmailAddress, DataType(DataType.EmailAddress)] string Email, 
    [Required, MinLength(6), MaxLength(25), DataType(DataType.Password)] string Password);