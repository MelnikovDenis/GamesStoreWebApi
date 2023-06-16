using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models;

public class Price
{
      public Guid PricedGameId { get; set; }
      public Game PricedGame { get; set; } 
      [Column(TypeName="date")]
      public DateTime StartDate { get; set; } = DateTime.UnixEpoch;
      [Required]
      [Column(TypeName="money")]
      public decimal Value { get; set; }
}