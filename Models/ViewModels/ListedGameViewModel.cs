namespace GamesStoreWebApi.Models.ViewModels;

public class ListedGameViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? PublisherName { get; set; }
    public string? DeveloperName { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal? CurrentDiscount { get; set; }
    public int KeyCount { get; set; }
    public ListedGameViewModel(Guid id, 
        string title, 
        string? description, 
        string? publisherName, 
        string? developerName, 
        DateOnly releaseDate, 
        decimal currentPrice, 
        decimal? currentDiscount, 
        int keyCount)
    {
        Id = id;
        Title = title;
        Description = description;
        PublisherName = publisherName;
        DeveloperName = developerName;
        ReleaseDate = releaseDate;
        CurrentPrice = currentPrice;
        CurrentDiscount = currentDiscount;
        KeyCount = keyCount;
    }
}
