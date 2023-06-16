using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models;
public class Game
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public Guid? Id { get; set; }
      [Required]
      public string Title { get; set; }
      public string? Description { get; set; }      
      public Company? Publisher { get; set; } 
      public Company? Developer { get; set; }
      [Required]
      [Column(TypeName="date")]
      public DateTime ReleaseDate { get; set; }
}