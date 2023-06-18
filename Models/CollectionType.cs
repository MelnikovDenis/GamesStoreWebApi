using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GamesStoreWebApi.Models;

public class CollectionType
{      
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public Guid Id { get; set; }
      [Required]
      public string Type { get; set; }
      public List<Collection>? Collections { get; set; }
}