using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models.Entities;

public class Price
{
    public Guid PricedGameId { get; set; }
    public Game PricedGame { get; set; }
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; } = DateTime.UnixEpoch;
    [Column(TypeName = "money"), Required]
    public decimal Value { get; set; }
}