using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models.Entities;

public class Key
{
    [Key]
    public Guid KeyId { get; set; }
    [Required]
    public Game KeyGame { get; set; }
    public Purchase? KeyPurchase { get; set; }
}