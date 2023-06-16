using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models;
public class Discount
{
      public Guid DiscountedGameId { get; set; }
      public Game DiscountedGame { get; set; }
      [Column(TypeName="date"), Required]
      public DateTime StartDate { get; set; }
      [Column(TypeName="date"), Required]
      public DateTime EndDate { get; set; }
      [Column(TypeName="decimal(5, 2)"), Required]
      public decimal Percent { get; set; }
}