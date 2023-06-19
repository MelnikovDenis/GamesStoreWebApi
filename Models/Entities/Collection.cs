namespace GamesStoreWebApi.Models.Entities;

public class Collection
{
    public Guid CollectionedGameId { get; set; }
    public Game CollectionedGame { get; set; }
    public Guid CollectionerId { get; set; }
    public ApplicationUser Collectioner { get; set; }
    public Guid TypeId { get; set; }
    public CollectionType Type { get; set; }
}