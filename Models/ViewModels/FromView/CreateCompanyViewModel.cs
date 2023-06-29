using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.FromView;

public record class CreateCompanyViewModel([Required] string Name, string? Description);