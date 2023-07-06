using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models.Entities;
public class Game
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public Company? Publisher { get; set; }
    public Company? Developer { get; set; }
    [Column(TypeName = "date"), Required]
    public DateTime ReleaseDate { get; set; }
    public List<Price>? Prices { get; set; }
    public List<Discount>? Discounts { get; set; }
    public List<Collection>? Collections { get; set; }
    public List<Key>? Keys { get; set; }
}