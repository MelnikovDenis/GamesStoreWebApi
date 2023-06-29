using System.ComponentModel.DataAnnotations;

namespace GamesStoreWebApi.Models.ViewModels.Shared;
public record class CompanyViewModel([Required] Guid Id, [Required] string Name, string? Description);
