using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesStoreWebApi.Models.Entities;

public class Purchase
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public ApplicationUser Purchaser { get; set; }
    [Required, Column(TypeName = "char(16)")]
    public string BankCard { get; set; }
    [Required]
    public DateTime PurchaseTime { get; set; }
    public List<Key> Keys { get; set; }
}