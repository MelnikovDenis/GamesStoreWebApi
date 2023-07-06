using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models.Entities;

public class Company
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Game>? PublisherGames { get; set; }
    public List<Game>? DeveloperGames { get; set; }
}