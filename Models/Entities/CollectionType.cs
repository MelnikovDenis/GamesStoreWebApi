using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models.Entities;
[Table("CollectionTypes")]
public class CollectionType
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Type { get; set; }
    public List<Collection>? Collections { get; set; }
}