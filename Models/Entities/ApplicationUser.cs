using Microsoft.AspNetCore.Identity;
namespace GamesStoreWebApi.Models.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public List<Collection>? Collections { get; set; }
    public List<Purchase>? Purchases { get; set; }
}