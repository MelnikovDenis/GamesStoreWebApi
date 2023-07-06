using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models.Entities;

public class RefreshToken
{
    [Required, Key]
    public Guid Token { get; init; } = Guid.NewGuid();
    [Required]
    public DateTime Created { get; init; } = DateTime.UtcNow;
    [Required]
    public DateTime Expires { get; init; }
    [Required]
    public ApplicationUser? User { get; set; } = null;
}
